$(document).ready(

    function ()
    {
        $('#departmentGroup').hide();
        $('#organizationGroup').hide();
        $('#userRole').change(
            function () {
                var userRole = $('#userRole').val();

                if ((userRole == 'WVUEmployee') || (userRole == 'ParkingEmployee')) {
                    $('#departmentGroup').show();
                    $('#organizationGroup').hide();
                }
                else if (userRole == 'Visitor') {
                    $('#organizationGroup').show();
                    $('#departmentGroup').hide();
                }
                else {
                    $('#departmentGroup').hide();
                    $('#organizationGroup').hide();
                }
            }
        );
}

);