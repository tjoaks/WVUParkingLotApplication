using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Models.LotModel;
using DiscussionMVCAppOaks.Models.LotTypeModel;
using DiscussionMVCAppOaks.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiscussionMVCAppOaks.Controllers
{
    public class LotsController : Controller
    {
        private ILotRepo iLotRepo;
        private ILotTypeRepo iLotTypeRepo;

        public LotsController(
            ILotRepo lotRepo, ILotTypeRepo lotTypeRepo)
        {
            this.iLotRepo = lotRepo;
            this.iLotTypeRepo = lotTypeRepo;
        }
        [Authorize(Roles = "WVUEmployee, ParkingEmployee")]
        public IActionResult ShowAllLots()
        {
            List<Lot> lotList = iLotRepo.ListAllLots();

            return View(lotList);
        }

        [HttpGet]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AddLot()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AddLot(Lot lot)
        {
            if (ModelState.IsValid)
            {
                iLotRepo.AddLot(lot).Wait();
                return RedirectToAction("ShowAllLots");
            }

            else
            {
                return View(lot);
            }
        }

        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult EditLot(int? lotID)
        {
            Lot lot = iLotRepo.FindLot(lotID);

            return View(lot);
        }

        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult EditLot(Lot lot)
        {
            if (ModelState.IsValid)
            {
                iLotRepo.EditLot(lot).Wait();
                return RedirectToAction("ShowAllLots");
            }

            else
            {
                return View(lot);
            }

        }

        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult DeleteLot(Lot lot)
        {

            iLotRepo.DeleteLot(lot).Wait();
            return RedirectToAction("ShowAllLots");

        }


        public IActionResult ConfirmDeleteLot(int? lotID)
        {
            Lot lot = iLotRepo.FindLot(lotID);
            return View(lot);
        }

        [HttpGet]
        public IActionResult SearchLots()
        {
            ViewData["LotTypes"] =
                new SelectList(iLotTypeRepo.ListAllLotTypes(), "LotTypeID", "LotTypeName");

            LotSearchViewModel searchViewModel = new LotSearchViewModel();

            return View(searchViewModel);
        }


       // [HttpPost]
        public IActionResult SearchLotsResult(string searchButton, string sortOrder, bool IsLotCurrentlyAvailable, string TypeOfDay, int ? LotTypeID, int pageNumber, LotSearchViewModel searchViewModel, int InputPageSize = 2)
        {

            ViewData["LotNameSortParameter"] = String.IsNullOrEmpty(sortOrder) ? "lotname_desc" : "";
            ViewData["CurrentSortOrder"] = sortOrder;

            ViewData["LotTypes"] =
                new SelectList(iLotTypeRepo.ListAllLotTypes(), "LotTypeID", "LotTypeName");

            if (searchButton == "Active")
            {
                IsLotCurrentlyAvailable = searchViewModel.IsLotCurrentlyAvailable;
                ViewData["IsLotCurrentlyAvailable"] = IsLotCurrentlyAvailable;

                TypeOfDay = searchViewModel.TypeOfDay;
                ViewData["TypeOfDay"] = TypeOfDay;

                LotTypeID = searchViewModel.LotTypeID;
                ViewData["LotTypeID"] = LotTypeID;

               // InputPageSize = searchViewModel.InputPageSize;
                ViewData["InputPageSize"] = InputPageSize;

            }
            else
            {
                ViewData["IsLotCurrentlyAvailable"] = IsLotCurrentlyAvailable;
                ViewData["TypeOfDay"] = TypeOfDay;
                ViewData["LotTypeID"] = LotTypeID;
                ViewData["InputPageSize"] = InputPageSize;
            }
            


            List<Lot> listOfLots = iLotRepo.ListAllLots();

            if (searchViewModel.IsLotCurrentlyAvailable)
            {
                listOfLots = listOfLots.Where
                (
                    l => l.TotalSpots > l.CurrentlyOccupiedSpots
                ).ToList();
            }

            if (searchViewModel.TypeOfDay != null)
            {
                listOfLots = listOfLots.Where
                (
                    l => l.LotStatuses.Any(ls => ls.TypeOfDay == searchViewModel.TypeOfDay)
                ).ToList();
            }

            if (searchViewModel.LotTypeID != null)
            {
                listOfLots = listOfLots.Where
                (
                    l => l.LotStatuses.Any(ls => ls.LotTypeID
                    == searchViewModel.LotTypeID)

                ).ToList();
            }

            switch(sortOrder)
            {
                case "lotname_desc":
                    listOfLots = listOfLots.OrderByDescending(l => l.LotName).ToList();
                    ViewData["LotNameImage"] = "descending";
                    break;

                default:
                    listOfLots = listOfLots.OrderBy(l => l.LotName).ToList();
                    break;

            }

            int totalItems = listOfLots.Count;
            int pageSize = InputPageSize;
         //   int pageNumber = 2;
            int excludeRows = (pageSize * pageNumber) - pageSize;

            listOfLots = listOfLots.Skip(excludeRows).Take(pageSize).ToList();

            searchViewModel.LotSearchResult.Data = listOfLots;
            searchViewModel.LotSearchResult.PageNumber = pageNumber;
            searchViewModel.LotSearchResult.PageSize = pageSize;
            searchViewModel.LotSearchResult.TotalItems = totalItems;


            return View("SearchLots", searchViewModel);
        }


    }
}
