var dps1 = [];
var dataPoints = "";
var currentTime = new Date();
var year = currentTime.getFullYear().toString();

var previousyear = currentTime.getFullYear() - 1;
previousyear = previousyear.toString();
//var chartList = JSON.parse('@Html.Raw(Json.Encode(Model.chartList))');

function addSymbols(e) {
    var suffixes = ["", "K", "M", "B"];

    var order = Math.max(Math.floor(Math.log(e.value) / Math.log(1000)), 0);
    if (order > suffixes.length - 1)
        order = suffixes.length - 1;

    var suffix = suffixes[order];
    return CanvasJS.formatNumber(e.value / Math.pow(1000, order)) + suffix;
}

var chart = new CanvasJS.Chart("chartContainer", {
    animationEnabled: true,
    theme: "light1",
    width: 520,
    backgroundColor: "#ECF0F5",
    colorSet: "greenShades",
    title: {
        text: "IMS Achive and Growth Status on " + ReportDate + "(MTD) ",
        fontSize: 16,
        margin: 25,
        fontFamily: "tahoma",
        padding: 10,
        fontWeight: "bold"

    },
    axisY: {
        title: "Crore",
        titleFontSize: 16,
        gridThickness: 0,
        stripLines: [
            {
                value: 0,
                showOnTop: true,
                color: "gray",
                thickness: 2
            }
        ],
        labelFormatter: function () {
            return " ";
        }
    },
    axisX: {
        labelAngle: -30
    },

    data: [{
        bevelEnabled: true,
        indexLabelFontSize: 16,
        type: "column",
        yValueFormatString: "#,###.00 Crore",

        dataPoints: [
            { y: parseFloat(mtdPymSalesVal), label: "IMS-" + previousyear, color: "#0D3996", indexLabel: parseFloat(mtdPymSalesVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(mtdCymTargetVal), label: "Target-" + currentTime.getFullYear(), indexLabel: parseFloat(mtdCymTargetVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(mtdImsSalesVal), label: "Actual IMS-" + currentTime.getFullYear(), color: "green", indexLabel: parseFloat(mtdImsSalesVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(mtdAchPct), label: "IMS ach%", indexLabel: mtdAchPct + "%" },
            { y: Math.abs(parseFloat(mtdGrowth)), label: "IMS Growth", indexLabel: mtdGrowth + "%" }
        ]
    }]
});

var chart1 = new CanvasJS.Chart("chartContainer1", {
    animationEnabled: true,
    theme: "light1",
    width: 520,
    backgroundColor: "#ECF0F5",
    title: {
        text: "IMS Achive and Growth Status from 01 Jan " + year.substring(year.length - 2) + " to  " + ReportDate + "(YTD) ",
        fontSize: 16,
        margin: 20,
        fontFamily: "tahoma",
        padding: 10,
        fontWeight: "bold",

    },
    axisY: {
        title: "Crore",
        titleFontSize: 16,

        gridThickness: 0,
        stripLines: [
            {
                value: 0,
                showOnTop: true,
                color: "gray",
                thickness: 2
            }
        ],
        labelFormatter: function () {
            return " ";
        }
    },
    axisX: {
        labelAngle: -30
    },

    data: [{
        bevelEnabled: true,
        indexLabelFontSize: 16,
        type: "column",
        yValueFormatString: "#,###.00 Crore",

        dataPoints: [
            { y: parseFloat(ytdPySalesVal), label: "IMS-" + previousyear, color: "#0D3996", indexLabel: parseFloat(ytdPySalesVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(ytdTargetVal), label: "Target-" + currentTime.getFullYear(), indexLabel: parseFloat(ytdTargetVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(ytdCyImsSalesVal), label: "Actual IMS-" + currentTime.getFullYear(), color: "green", indexLabel: parseFloat(ytdCyImsSalesVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(ytdAchPct), label: "IMS ach%", indexLabel: ytdAchPct + "%" },
            { y: Math.abs(parseFloat(ytdGrowth)), label: "IMS Growth", indexLabel: ytdGrowth + "%" }
        ]
    }]
});


var chart2 = new CanvasJS.Chart("chartContainer2", {
    animationEnabled: true,
    theme: "light1",
    width: 520,
    backgroundColor: "#ECF0F5",
    title: {
        text: "Achive and Growth Status on Budget on " + ReportDate + "(MTD,Lifting) ",
        fontSize: 16,
        margin: 20,
        fontFamily: "tahoma",
        padding: 10,
        fontWeight: "bold",
    },
    axisY: {
        title: "Crore",
        titleFontSize: 16,
        gridThickness: 0,
        stripLines: [
            {
                value: 0,
                showOnTop: true,
                color: "gray",
                thickness: 2
            }
        ],
        labelFormatter: function () {
            return " ";
        }
    },
    axisX: {
        labelAngle: -30
    },

    data: [{
        //bevelEnabled: true,
        indexLabelFontSize: 16,
        type: "column",
        yValueFormatString: "#,###.00 Crore",

        dataPoints: [
            { y: parseFloat(liftingPymActualValue), label: "Lifting-" + previousyear, color: "#0D3996", indexLabel: parseFloat(liftingPymActualValue, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(liftingTargetVal), label: "Target-" + currentTime.getFullYear(), indexLabel: parseFloat(liftingTargetVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(liftingCymActualValue), label: "Actual Lifting-" + currentTime.getFullYear(), color: "green", indexLabel: parseFloat(liftingCymActualValue, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(liftingAchPct), label: "Lifting ach%", indexLabel: liftingAchPct + "%" },
            { y: Math.abs(parseFloat(liftingGrowth)), label: "Growth", indexLabel: liftingGrowth + "%" }
        ]
    }]
});

var chart3 = new CanvasJS.Chart("chartContainer3", {
    animationEnabled: true,
    theme: "light1",
    width: 520,
    backgroundColor: "#ECF0F5",
    title: {
        text: "Achive and Growth Status on Budget from 01 Jan  " + year.substring(year.length - 2) + " to " + ReportDate + "(YTD,Lifting) ",
        fontSize: 16,
        margin: 20,
        fontFamily: "tahoma",
        padding: 10,
        fontWeight: "bold"
    },
    axisY: {
        title: "Crore",
        titleFontSize: 16,
        gridThickness: 0,
        stripLines: [
            {
                value: 0,
                showOnTop: true,
                color: "gray",
                thickness: 2
            }
        ],
        labelFormatter: function () {
            return " ";
        }
    },
    axisX: {
        labelAngle: -30
    },

    data: [{
        //bevelEnabled: true,
        indexLabelFontSize: 16,
        type: "column",
        yValueFormatString: "#,###.00 Crore",

        dataPoints: [
            { y: parseFloat(liftingYearlyPymActualValue), label: "Lifting-" + previousyear, color: "#0D3996", indexLabel: parseFloat(liftingYearlyPymActualValue, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(liftingYearlyTargetVal), label: "Target-" + currentTime.getFullYear(), indexLabel: parseFloat(liftingYearlyTargetVal, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(liftingYearlyCymActualValue), label: "Actual Lifting-" + currentTime.getFullYear(), color: "green", indexLabel: parseFloat(liftingYearlyCymActualValue, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() },
            { y: parseFloat(liftingYearlyAchPct), label: "Lifting ach%", indexLabel: liftingYearlyAchPct + "%" },
            { y: Math.abs(parseFloat(liftingYearlyGrowth)), label: "Growth", indexLabel: liftingYearlyGrowth + "%" }
        ]
    }]
});
$(document).ready(function () {


    chart.render();
    chart1.render();
    chart2.render();
    chart3.render();

})