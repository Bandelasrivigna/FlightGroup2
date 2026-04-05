// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    const today = moment();
    const selectedDate = $('#ActiveDepartureDate').val();

    const startDate = selectedDate
        ? moment(selectedDate)
        : today.clone().add(1, 'days');

    $('#ActiveDepartureDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: today,
        startDate: startDate,
        locale: {
            format: 'YYYY-MM-DD'
        }
    });
});