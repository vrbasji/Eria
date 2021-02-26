setInterval(updateDisplay, 1000);

function updateDisplay() {
    $('.timer').each(function () {
        var startTime = $(this).parent().parent().find(".startTimer");
        var start = Date.parse(startTime.text());
        if (!isNaN(start)) {
            var res = getTimeDiference(start, Date.now());
            console.log(res);
            $(this).find('.value').text(toStringTime(res));
        }
    });
};
function getTimeDiference(startDate, endDate) {
    return (endDate - startDate) / 1000;
}
function toStringTime(seconds) {
    var sec_num = parseInt(seconds, 10);
    var hours = Math.floor(sec_num / 3600);
    var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
    var seconds = sec_num - (hours * 3600) - (minutes * 60);

    if (hours < 10) { hours = "0" + hours; }
    if (minutes < 10) { minutes = "0" + minutes; }
    if (seconds < 10) { seconds = "0" + seconds; }
    return hours + ':' + minutes + ':' + seconds;
}
$(function () {
    $("input[type=checkbox]").on("click", function () {
        if ($("#stopWatch").is(':checked')) {
            $("#TimeTo").prop('disabled', true);
        } else {
            $("#TimeTo").prop('disabled', false);
        }
    });
});