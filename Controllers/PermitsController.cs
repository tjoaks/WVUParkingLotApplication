using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Models;
using DiscussionMVCAppOaks.Models.LotModel;
using DiscussionMVCAppOaks.Models.LotStatusModel;
using DiscussionMVCAppOaks.Models.PermitModel;
using DiscussionMVCAppOaks.ViewModel;
//using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using GoogleApi.Entities.Maps.StaticMaps.Response;
using DiscussionMVCAppOaks.Services;

namespace DiscussionMVCAppOaks.Controllers
{
    public class PermitsController : Controller
    {
        private IPermitRepo iPermitRepo;
        private ILotRepo iLotRepo;
        private ILotStatusRepo iLotStatusRepo;
        private IApplicationUserRepo iApplicationUserRepo;

        //ctor <tab> <tab>
        public PermitsController(IPermitRepo permitRepo, ILotRepo lotRepo,
            ILotStatusRepo lotStatusRepo, IApplicationUserRepo applicationUserRepo)
        {
            this.iPermitRepo = permitRepo;
            this.iLotRepo = lotRepo;
            this.iLotStatusRepo = lotStatusRepo;
            this.iApplicationUserRepo = applicationUserRepo;

        }

        public FileResult DisplayParkingLotsOnMap()
        {
            List<Lot> allLots = iLotRepo.ListAllLots();
            List<MapMarker> mapMarkers = new List<MapMarker>();

            foreach(Lot eachLot in allLots)
            {
                MapMarker eachMapMarker = new MapMarker();

                List<Location> locations = new List<Location>();
                Location eachLocation = new Location(eachLot.LotAddress);
                locations.Add(eachLocation);

                eachMapMarker.Locations = locations;

                eachMapMarker.Label = eachLot.LotNumber; // can only be A-Z or 0-9

                eachMapMarker.Color = GoogleApi.Entities.Maps.StaticMaps.Request.Enums.MapColor.Red;

                mapMarkers.Add(eachMapMarker);
            }

            StaticMapsRequest staticMapsRequest = new StaticMapsRequest();

            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("WvuEmployeeID")))
            {
                string WvuEmployeeID = HttpContext.Session.GetString("WvuEmployeeID");
                WVUEmployee wvuEmployee = iApplicationUserRepo.FindWvuEmployee(WvuEmployeeID);
                string origin = wvuEmployee.Department.DepartmentAddress;

                MapMarker eachMapMarker = new MapMarker();

                List<Location> locations = new List<Location>();
                Location employeeLocation = new Location(origin);
                locations.Add(employeeLocation);

                eachMapMarker.Locations = locations;

                eachMapMarker.Label = "E"; // can only be A-Z or 0-9

                eachMapMarker.Color = GoogleApi.Entities.Maps.StaticMaps.Request.Enums.MapColor.Red;

                mapMarkers.Add(eachMapMarker);

                staticMapsRequest.Center = employeeLocation;
            }  
            
            staticMapsRequest.Key = "AIzaSyCJXs4iSqvnKFnHOcuaWUPWUPeok5iHRlI";

            staticMapsRequest.Markers = mapMarkers;

            staticMapsRequest.Type = GoogleApi.Entities.Maps.StaticMaps.Request.Enums.MapType.Roadmap;

            StaticMapsResponse response = GoogleApi.GoogleMaps.StaticMaps.Query(staticMapsRequest);

            var file = response.Buffer;

            return File(file, "image/jpeg");


        }

        public void DropDownListForEmployees()
        {
            ViewData["Employees"] =
                new SelectList(iApplicationUserRepo.ListWvuEmployees().OrderBy(w => w.Fullname), "Id", "EmployeeNameAndDepartment");
        }

        [HttpGet]
        public IActionResult DetermineDistanceMatrix()
        {
            DropDownListForEmployees();
            DistanceMatrixViewModel viewModel = new DistanceMatrixViewModel();
            return View(viewModel);
        }

   //     [HttpPost]
        public IActionResult DetermineDistanceMatrixResult(string sortOrder, DistanceMatrixViewModel inputViewModel)
        {

            // if()? true : false
            ViewData["DistanceSortParam"] = String.IsNullOrEmpty(sortOrder) ? "distance_desc" : "";

            ViewData["DurationSortParam"] = sortOrder == "duration" ? "distance_desc" : "duration";

            string WvuEmployeeID = inputViewModel.WvuEmployeeID;

            // to use session objects, add to startup
            // services.addsession
            // app.usesession
            
            if(WvuEmployeeID != null)
            {
                HttpContext.Session.SetString("WvuEmployeeID", WvuEmployeeID);
            }

            if(!String.IsNullOrEmpty(HttpContext.Session.GetString("WvuEmployeeID") ))
            {
                WvuEmployeeID = HttpContext.Session.GetString("WvuEmployeeID");
            }

            WVUEmployee wvuEmployee = iApplicationUserRepo.FindWvuEmployee(WvuEmployeeID);
            string origin = wvuEmployee.Department.DepartmentAddress;
          //  string destination;

            if(inputViewModel.WvuEmployeeID != null)
            {
                inputViewModel = CreateDistanceMatrix(origin);
                HttpContext.Session.SetComplexData("inputViewModel", inputViewModel);
            }
            else
            {                
                inputViewModel = HttpContext.Session.GetComplexData<DistanceMatrixViewModel>("inputViewModel");
            }

            

            switch(sortOrder)
            {
                case "distance_desc":
                    inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderByDescending(d => d.DistanceInMiles).ToList();
                    ViewData["DistanceImage"] = "descending";

                    break;

                case "duration" : inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderBy(d => d.DurationInMinutes).ToList();
                    break;

                case "duration_desc":
                    inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderByDescending(d => d.DurationInMinutes).ToList();
                    break;

                default:
                    inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderBy(d => d.DistanceInMiles).ToList();
                    break;
            }

            inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderBy(d => d.DistanceInMiles).ToList();

            DropDownListForEmployees();

            return View("DetermineDistanceMatrix", inputViewModel);
        }

        public DistanceMatrixViewModel CreateDistanceMatrix(string origin)
        {
        
        DistanceMatrixViewModel inputViewModel = new DistanceMatrixViewModel();
            inputViewModel.DistanceMatrix = new List<DistanceMatrixViewModel>();

        List<Location> destinationLocations = new List<Location>();

        List<Lot> allLots = iLotRepo.ListAllLots();

            foreach (Lot eachLot in allLots)
            {
                DistanceMatrixViewModel processingViewModel = new DistanceMatrixViewModel();
                processingViewModel.LotID = eachLot.LotID;
                processingViewModel.LotNumber = eachLot.LotNumber;
                processingViewModel.LotName = eachLot.LotName;
                processingViewModel.LotAddress = eachLot.LotAddress;
                processingViewModel.AvailableSpots = eachLot.TotalSpots - eachLot.CurrentlyOccupiedSpots;
                inputViewModel.DistanceMatrix.Add(processingViewModel);

                Location destinationLocation = new Location(eachLot.LotAddress);

        destinationLocations.Add(destinationLocation);
                
            }

    DistanceMatrixResponse response = FindDistanceAndDurationBetweenOriginAndDestination(origin, destinationLocations);

    Row row = response.Rows.FirstOrDefault();
    List<Element> elements = row.Elements.ToList();
    int distanceInMeters;
    double distanceInMiles;
    const double metersToMilesConverter = 0.00062137;
    int durationInSeconds;
    int durationInMinutes;
    int index = 0;

            foreach(Element eachElement in elements)
            {
                distanceInMeters = eachElement.Distance.Value;
                distanceInMiles = Math.Round((distanceInMeters* metersToMilesConverter), 2);
                durationInSeconds = eachElement.Duration.Value;
                durationInMinutes = durationInSeconds / 60;
                inputViewModel.DistanceMatrix[index].DistanceInMiles = distanceInMiles;
                inputViewModel.DistanceMatrix[index].DurationInMinutes = durationInMinutes;
                index++;
            }
            return inputViewModel;

} //end of CreateDistanceMatrix method

        public DistanceMatrixResponse FindDistanceAndDurationBetweenOriginAndDestination(string origin, List<Location> destinationLocations)
        {
           // string origin = "1601 University Ave, Morgantown, WV 26506";
          // string destination = "2001 Rec Center Dr, Morgantown, WV 26506";

            DistanceMatrixRequest request = new DistanceMatrixRequest();

            request.Key = "AIzaSyCJXs4iSqvnKFnHOcuaWUPWUPeok5iHRlI";

            List<Location> originLocations = new List<Location>();
            Location originLocation = new Location(origin);
            originLocations.Add(originLocation);

            request.Origins = originLocations;
            request.Destinations = destinationLocations;

            var response = GoogleApi.GoogleMaps.DistanceMatrix.Query(request);
            return response;

        }

        public IActionResult ListAllPermits()
        {
            List<Permit> allPermits = iPermitRepo.ListAllPermits();
            return View(allPermits);

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AssignPermit()
        {
            ViewData["Lots"] = new SelectList(iLotRepo.ListAllLots(), "LotID", "LotName");


            ViewData["Employees"] = new SelectList(iApplicationUserRepo.ListWvuEmployees(), "Id", "Fullname");

            AssignPermitViewModel viewModel = new AssignPermitViewModel();

            viewModel.StartDate = DateTime.Today;

            return View(viewModel);
        }



        [HttpPost]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AssignPermit(AssignPermitViewModel viewModel)
        {
            //user is wvu parkingemployee
            string parkingEmployeeID = iApplicationUserRepo.FindUserID();

            string errorMessage = "None";

            bool employeeHasPermit = iPermitRepo.DoesWvuEmployeeHavePermit(viewModel.WvuEmployeeID);

            if (employeeHasPermit)
            {
                errorMessage = "Employee already has a permit";
                ModelState.AddModelError("EmployeePermit", errorMessage);
            }

            bool lotAvailable = iLotRepo.IsChosenLotAvailable(viewModel.LotID);

            if (!lotAvailable)
            {
                errorMessage = "Lot is not available";
                ModelState.AddModelError("LotAvailable", errorMessage);
            }
            if (!employeeHasPermit && lotAvailable)
            {
                int lotTypeID = 3; // permit lot type
                double permitAmount = iLotStatusRepo.FindPermitAmount(viewModel.LotID, lotTypeID);


                // DateTime startDate =  //DateTime.Now.Date;
                DateTime endDate = viewModel.StartDate.AddYears(1);

                // assign permit if no permit

                Permit permit = new Permit(permitAmount, viewModel.StartDate, endDate, viewModel.WvuEmployeeID, parkingEmployeeID);

                iPermitRepo.AddPermit(permit).Wait();

                
                Lot lot = iLotRepo.FindLot(viewModel.LotID);

                // lot object - CurrentOccupancy to +1.
                lot.CurrentlyOccupiedSpots += 1;

                iLotRepo.EditLot(lot).Wait();

                return RedirectToAction("ListAllPermits");

            }
            else
            {

                ViewData["Lots"] = new SelectList(iLotRepo.ListAllLots(), "LotID", "LotName");

                ViewData["Employees"] = new SelectList(iApplicationUserRepo.ListWvuEmployees(), "Id", "Fullname");

                return View();
            }
        }
    }
}