jQuery.validator.addMethod("legitimatedate", function (value, element, param) {
    if (value === '') return false;

    var dateToCheck = new Date(value);
    if (isNaN(dateToCheck.getTime())) return false;

    var maxYears = Number(param);

    var today = new Date();
    today.setHours(0, 0, 0, 0);

    var maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() + maxYears);
    maxDate.setHours(0, 0, 0, 0);

    return (dateToCheck > today && dateToCheck <= maxDate);
});
jQuery.validator.unobtrusive.adapters.addSingleVal("legitimatedate", "years");