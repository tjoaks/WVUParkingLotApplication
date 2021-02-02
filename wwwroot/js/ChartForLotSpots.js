var element = $('#chartForLotSpots');

$.ajax(
    {
        url: 'Home/GetSpotsForAllLots',
        type: 'get',
        dataType: 'json',
        data: {},
        success: function (data) {
            CreateBarChart(data, element);
        },
        error: function () {
            alert('Data Not Received');
        }
    }
);

function CreateBarChart(inputData, inputElement) {
    new Morris.Bar(
        {
            element: inputElement,
            data: inputData,
            xkey: ['LotName'],
            ykeys: ['TotalSpots', 'CurrentlyOccupiedSpots'],
            labels: ['TotalSpots', 'CurrentlyOccupiedSpots'],
            xLabelMargin: 10,
            hideHover: true,
            barSizeRatio: 0.5,
            barGap: 5
        }
    );
}