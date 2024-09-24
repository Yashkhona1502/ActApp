function EnableButton() {
    $("#btnUpload").removeAttr('disabled');
    $('#loading-spinner').show();
    $.ajax({
        type: 'Post',
        url: '/Accomodation/GetHotelList',
        data: { "eventId": $('#ddlEvent').val() },
        success: function(data){
            //alert(data);
            $("#hotel-list").html(data);
            var hotelModel = data;
            $.ajax({
                type: 'Post',
                url: '/Accomodation/GetHotelListView',
                data: JSON.stringify(hotelModel),
                dataType: 'JSON',
                contentType: "application/json; charset =utf-8",

                success: function (data) {
                    setTimeout(function () {
                        if (data.length != 0) {
                            var tableHtml = '<table class="table table-striped" id="tbHotel"><thead></thead><tbody></tbody></table>';
                            $('#hotel-list').html(tableHtml);

                            var tableHeaders = ['Hotel', 'Total Room', 'Location', 'City', 'State', 'Country', 'Postal', 'Address', 'Cluster'];
                            var tableHeaderHtml = '';
                            $.each(tableHeaders, function (index, header) {
                                tableHeaderHtml += '<th>' + header + '</th>';
                            });
                            $('#tbHotel thead').html('<tr>' + tableHeaderHtml + '</tr>');
                            $.each(data, function (index, item) {
                                var row = '<tr>';
                                row += '<td>' + item.HotelName + '</td>';
                                row += '<td>' + item.TotalRoom + '</td>';
                                row += '<td>' + item.Location + '</td>';
                                row += '<td>' + item.City + '</td>';
                                row += '<td>' + item.State + '</td>';
                                row += '<td>' + item.Country + '</td>';
                                row += '<td>' + item.PostalCode + '</td>';
                                row += '<td>' + item.StreetAddress + '</td>';
                                row += '<td>' + item.ClusterName + '</td>';
                                row += '</tr>';
                                $('#tbHotel tbody').append(row);
                            });
                        }
                        $('#loading-spinner').hide();
                    }, 5000);
                }
            });
        }
    });
}
