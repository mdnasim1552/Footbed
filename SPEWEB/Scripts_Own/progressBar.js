/*
  Author:Shamim Shahrier Emon
  Date:07/08/2015

  steps:
    1.id a div with #pb
    2.link this file in aspx
    3. add following links in aspx: 
        <link href="../Content/jquery-ui.css" rel="stylesheet" />
        <script src="../Scripts/jquery-ui.js"></script>

    4.call StartProgressBar() to turn on progress bar
    5.call $("#pb").hide() to hide progress bar
     
*/


function StartProgressBar() {
    $("#pb").show();
    $("#pb").progressbar({ value: 100 });
    IndeterminateProgressBar($("#pb"));
}
function IndeterminateProgressBar() {
    $("#pb").css({ "padding-left": "0%", "padding-right": "20%" });
    $("#pb").progressbar("option", "value", 100);
    $("#pb").animate({ paddingLeft: "20%", paddingRight: "0%" }, 3000, "linear",
        function () { IndeterminateProgressBar(pb); });
}