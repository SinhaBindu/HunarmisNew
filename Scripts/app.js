$(function () {

    $("#captcha-div a").attr('href', '');
    $("#captcha-div a").on('click', function (event) {
        event.preventDefault();
    });
    $("#CaptchaInputText").attr('placeholder', 'Enter captcha code').attr('maxlength', '10');

});
$(document).ready(function () {
    $("#mainnav-menu li.treeview a[data-target-url]").click(function (e) {
        var url = $(this).attr('data-target-url');
        if (url != '#') {
            location.href = url;
        }
    });

    $(".treeview li.active-link").each(function (i, e) {
        $(this).parent('ul').addClass('in');
        //$(this).parent('ul').parent('li').parent('ul').parent('li').addClass('active');
        $(this).parents('li.treeview').addClass('active-sub active');
        //if ($(this).parents('li.treeview').parent('ul').parent('li').length) {
            $(this).parents('li.treeview').addClass('active').children('ul').addClass('in');
        //}
        
        //$(this).parents('li.treeview').children('a').addClass('active-sub-sub');
        //$(this).parents('li.treeview').parents('li.treeview').removeClass('active').addClass('active-sub');
    });
    $("#mainnav-menu li.active-link").addClass('active-sub');

});

$(function () {
    $("#btnPrint").click("click", function () {
        var printdiv = document.getElementById('printdiv');
        var popupWin = window.open('null', '_blank', 'width=500,height=500');
        popupWin.document.open();
        popupWin.document.write('<html> ' +
            '<link href="~/Content/bootstrap-table.css" rel="stylesheet" />' +
            '<link href="~/Content/bootstrap.css" rel="stylesheet" />' +

            '<style>#header, #nav, .noprint{display: none;} #Header, #Footer { display: none !important; }' +
            '.lbl{font-weight:bold; background-color:#bdb7b7; border:1px solid; font-size:10px; }' +
            ' .lbl1 {font-weight: bold; background-color: darkgray; border: 1px solid; font-size: 12px;}' +
            '.htxt{font-weight:bold; font-size:12px; text-align:left;}' +
            '  table {padding: 0px; margin: 0px; cellpadding: 0; cellspacing: 0; font-size: 11px;}' +
            'body{margin:0;padding:0px;}' +
            '@page{size: auto; margin-top:0mm; margin-bottom:0mm}</style>' +
            '<body onload="window.print()" style="font-size:12px;"></br>' + printdiv.innerHTML + '</html>');
        popupWin.document.close();
    });
});
$(document).ajaxStart(function () {
    console.log('g-ajaxStart')
    $("#panel-load").show();
});
$(document).ajaxStop(function () {
    $("#panel-load").hide();
});

$("form.loader").on('submit', function () {
    if ($(this).valid()) {
        $("#panel-load").show();
    }
});

$(function () {
    $(".req").append(" <em>*<em>");
    $("#ListDataTable").DataTable();

});

$(document).ready(function () {

    $('.namevalid').on('keypress', function (e) {
        var regex = new RegExp("^[a-zA-Z ]*$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        e.preventDefault();
        return false;
    });
    $('#CCEmail').on('change', function (e) {
        var regex = new RegExp("(([a-zA-Z\-0-9\.]+@)([a-zA-Z\-0-9\.]+)[,]*)+");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        e.preventDefault();
        return false;
    });
    $(document).on('invalid-form.validate', 'form', function () {
        var button = $(this).find('input[type="submit"]');
        setTimeout(function () {
            button.removeAttr('disabled');
        }, 1);
    });
    $(document).on('submit', 'form', function () {
        //var button = $(this).find('input[type="submit"]');
        var button = $(this).find('#btnSubmit');

        setTimeout(function () {
            // button.attr('disabled', 'disabled');
            $(button, this).val("Working...").attr('disabled', 'disabled');
        }, 0);
    });
    $(".scroll-bottom").click(function (e) {
        e.preventDefault();
        $('html,body').animate({ scrollTop: document.body.scrollHeight }, "medium");
    })
});
$(function () {
    //Initialize Select2 Elements
    $(".select2").select2({
        placeholder: "Search to Select"
    });
});

$(document).ready(function () {
    $('#loginform').on('submit', function () {
        if ($(this).valid()) {
            aesauth($('#login-password'));
            return true;
        }
    });
    $("form").attr('autocomplete', 'off');
    $('#change-pwd-form').on('submit', function () {
        if ($(this).valid()) {
            aesauth($('#OldPassword'));
            aesauth($('#NewPassword'));
            $('#ConfirmPassword').val($('#NewPassword').val());
            return true;
        }
    });
    $(".datepicker,.hasDatepicker").attr('readonly', 'readonly');

    $('#resetform').on('submit', function () {
        if ($(this).valid()) {
            aesauth($('#Password'));
            $('#ConfirmPassword').val($('#Password').val());
            return true;
        }
    });
});

function aesauth(element) {
    var dnum = CryptoJS.lib.WordArray.random(128 / 16).toString();
    var dnum2 = CryptoJS.lib.WordArray.random(128 / 16).toString();
    var zen = CryptoJS.enc.Utf8.parse(dnum.toString());

    var password = element.val();
    var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(password), zen,
        {
            keySize: 128 / 8,
            iv: zen,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });
    element.val(dnum.toString() + encryptedpassword + dnum2.toString());

}

$(document).ready(function () {
    $(document).on('focus', '.datetimepic', function () {

        $(this).datetimepicker({
            dateFormat: "dd-M-yy",
            minDate: "+0D",
            controlType: 'select',
            oneLine: true,
            timeFormat: 'hh:mm TT'
        });
    });

    $(".datepicker").datepicker({
        dateFormat: 'dd-M-yy',
        changeMonth: true,
        changeYear: true,
        maxDate: new Date()
    });
    $(".datepicker").datepicker({
        dateFormat: 'dd-M-yy',
        changeMonth: true,
        changeYear: true,
        maxDate: new Date()
    });
    $(".futuredatepicker").datepicker({
        dateFormat: 'dd-M-yy',
        changeMonth: true,
        changeYear: true,
        minDate: new Date()
    });

    var tommorow = new Date();
    tommorow.setDate(tommorow.getDate() + 1); // add a day

    $(".tommorow_datepicker").datepicker({
        dateFormat: 'dd-M-yy',
        changeMonth: true,
        changeYear: true,
        minDate: tommorow
    });
    $("input[type='text']").each(function () {
        $(this).attr("autocomplete", "off");
    });


    $("div").on('input', '.input_capital', function (evt) {

        // Remember original caret position
        var caretPosition = getCaretPosition(this);

        // Uppercase-ize contents
        this.value = this.value.toLocaleUpperCase();

        // Reset caret position
        // (we ignore selection length, as typing deselects anyway)
        setCaretPosition(this, caretPosition);
    });

});


function getCaretPosition(ctrl) {
    var CaretPos = 0;    // IE Support
    if (document.selection) {
        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;
    }
    // Firefox support
    else if (ctrl.selectionStart || ctrl.selectionStart === '0') {
        CaretPos = ctrl.selectionStart;
    }

    return CaretPos;
}

function setCaretPosition(ctrl, pos) {
    if (ctrl.setSelectionRange) {
        ctrl.focus();
        ctrl.setSelectionRange(pos, pos);
    }
    else if (ctrl.createTextRange) {
        var range = ctrl.createTextRange();
        range.collapse(true);
        range.moveEnd('character', pos);
        range.moveStart('character', pos);
        range.select();
    }
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function isDecimal(evt, value) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode === 46) {
        if (value.indexOf('.') > -1) {
            return false;
        }
        else {
            return true;
        }
    }
    else if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}


// Table Row Data
//1. Add new row
function AddNewRow(tblid) {

    var $tableBody = $('#' + tblid);

    if ($tableBody.find('tr').length > 30) {
        return false;
    }
    //var $trFirst = $("#first-sub-row");
    var $trLast = $tableBody.find("tr:last");
    var $trNew = $trLast.clone();



    var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);


    $trNew.find("td:first").html((parseInt(suffix) + 2));
    $trNew.find("td:last").html('<p class="add-sub btn btn-success btn-sm"  onclick="AddNewRow(\'' + tblid + '\')" >+</p>&nbsp;<p class="remove-sub btn btn-danger btn-sm" id="' + tblid + '_' + (parseInt(suffix) + 1) + '" onclick="RemoveRow(this.id,\'' + tblid + '\')" >-</p>');
    $.each($trNew.find(':input'), function (i, val) {
        // Replaced Name

        var $this = $(this);
        var oldN = $(this).attr('name');
        var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');

        try {
            var oldID = $(this).attr('id');
            var newID = oldID.replace('_' + suffix + '_', '_' + (parseInt(suffix) + 1) + '_');
        }
        catch (err) { console.error(err); }

        $(this).attr('name', newN);
        $(this).attr('id', newID);

        $(this).val('');
        $(this).removeClass('hasDatepicker');

        $(this).css('background-color', '#fff');

    });
    $.each($trNew.find('.zero_id'), function () {

        $(this).val(0);
    });

    $.each($trNew.find('.remove'), function () {

        $(this).html('');
    });

    $.each($trNew.find('span'), function () {
        try {
            var $this = $(this);
            var oldN = $(this).attr('data-valmsg-for');
            var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
            $(this).attr('data-valmsg-for', newN);
        }
        catch (err) {
            console.err(err);
        }
    });


    $trLast.after($trNew);
    //Re-assign Validation
    var form = $("form")
        .removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);

    // BindDate();

}

//2. Remove row
function RemoveRow(id, tblid) {

    $("#" + id).closest('tr').remove();

    var count = 1;
    var $tableBody = $('#' + tblid);

    $.each($tableBody.find("tbody tr"), function (i) {

        var $tr = $(this);

        $tr.find("td:first").html((i + 1));

        $.each($tr.find(':input'), function () {

            // Replaced Name
            var suffix = $(this).attr('name').match(/\d+/);
            var oldN = $(this).attr('name');
            var newN = oldN.replace('[' + suffix + ']', '[' + (i) + ']');

            $(this).attr('name', newN);

        });

        $.each($tr.find('p.remove-sub'), function () {
            $(this).attr('id', tblid + '_' + i);

        });


        $.each($tr.find('span'), function () {

            try {
                var suffix = $(this).attr('data-valmsg-for').match(/\d+/);
                var oldN = $(this).attr('data-valmsg-for');
                var newN = oldN.replace('[' + suffix + ']', '[' + (i) + ']');
                $(this).attr('data-valmsg-for', newN);
            }
            catch (err) { consol.error(err); }

        });
    })
}


var colorsConst = ['#4443a0', '#ae77b8', '#9be0ba', '#4194ac', '#44449e', '#47a372', '#3466ab', '#8a3b99'];
function Graph(jsonData, selector, graphType = 'column', title, xAxis, yAxis, property, groupProperty, colorsParam) {
    console.log(jsonData);
    if (!jsonData) {
        jsonData = [];
    }
    var totalEle = $('#' + selector + 'Total');
    if (totalEle.length) {
        var total = 0;
        if (jsonData && jsonData.length) {
            jsonData.forEach(item => {
                total += item.Total;
            });
        }
        $(totalEle).text(total);
    }
    var data = [];
    var categories = [];
    var series = [];
    var totals = [];
    var colorAxis = [];
    var originalGraphType = graphType;
    if (graphType == 'bar-group') {
        graphType = 'bar';

        categories = [...new Set(jsonData.map(item => item[property]))];
        const groups = [...new Set(jsonData.map(item => item[groupProperty]))];

        series = groups.map(key => ({
            name: key,
            data: categories.map(category => {
                const item = jsonData.find(d => d[property] === category && d[groupProperty] === key);
                return item ? item.Total : null;
            })
        }));

    }
    else if (graphType == 'stack-bar' || graphType == 'stack-column') {
        if (graphType == 'stack-bar') {
            graphType = 'bar';
        }
        else if (graphType == 'stack-column') {
            graphType = 'column';
        }

        // Prepare the series data
        const seriesData = {};
        categories = [...new Set(jsonData.map(item => item[property]))];
        jsonData.forEach(item => {
            if (!seriesData[item[groupProperty]]) {
                seriesData[item[groupProperty]] = [];
            }
            seriesData[item[groupProperty]].push({
                name: item[property],
                y: item.Total,
                percentage: item.Percentage
            });
        });

        // Convert series data into Highcharts format
        series = Object.keys(seriesData).map(gender => ({
            name: gender,
            data: seriesData[gender]
        }));

        // Calculate total per course
        totals = {};
        jsonData.forEach(item => {
            if (!totals[item[property]]) {
                totals[item[property]] = 0;
            }
            totals[item[property]] += item.Total;
        });

        console.log(totals);
    }
    else if (graphType == 'column-group') {
        graphType = 'column';


        categories = [...new Set(jsonData.map(item => item[property]))];
        const groups = [...new Set(jsonData.map(item => item[groupProperty]))];

        series = groups.map(key => ({
            name: key,
            dataLabels: {
                allowOverlap: true,
                rotation: 270,
                crop: false,
            },
            data: categories.map(category => {
                const item = jsonData.find(d => d[property] === category && d[groupProperty] === key);
                return item ? item.Total : null;
            })
        }));
        console.log(series);
    }
    else if (graphType == 'pie' || graphType == 'donut') {
        graphType = 'pie';
        categories = [...new Set(jsonData.map(item => item[property]))];
        //categories = jsonData.map(item => item[property]);
        const totals = jsonData.map(item => item.Total);

        var seriesData = jsonData.map(item => ({
            name: item[property],
            y: item.Percentage,
            total: item.Total, // Add total to each data point
        }));
        series = [{
            name: 'Percentage',
            //colorByPoint: true,
            innerSize: originalGraphType == 'pie' ? '0%' : '40%',
            data: seriesData
        }];

        console.log(series);
    }
    else if (graphType == 'treemap') {
        categories = [...new Set(jsonData.map(item => item[property]))];
        colorAxis = [];
        for (var i = 0; i < categories.length; i++) {
            colorAxis.push({ minColor: colorsConst[i], maxColor: colorsConst[i] })
        }
        var i = categories.length;
        var seriesData = jsonData.map(item => ({
            name: item[property],
            value: item.Total,
            total: item.Total,
            percentage: item.Percentage,
            colorValue: i--,
            //color: colorsConst[i],
        }));
        series = [{
            type: 'treemap',
            //layoutAlgorithm: 'squarified',
            layoutAlgorithm: 'sliceAndDice',
            allowDrillToNode: true,
            animationLimit: 1000,
            clip: false,
            data: seriesData,
            dataLabels: {
                enabled: true,
                formatter: function () {
                    var key = this.key,
                        point = this.point,
                        value = point.value,
                        percentage = point.percentage;

                    return value ? `${key} (${percentage}%)` : key;
                }
            }
        }];
        console.log(series);
    }
    else {

        categories = jsonData.map(item => item[property]);
        const totals = jsonData.map(item => item.Total);

        series = [{
            showInLegend: false,
            name: property,
            colorByPoint: true,
            dataLabels: {
                format: '{point.y:.0f}',
            },
            data: totals
        }];
        console.log(series);
    }
    //colors = ['#4443a0', '#ae77b8', '#1D4E89', '#1C558E', '#1A5B92', '#16679A', '#0F80AA', '#2ec4b6'];
    colors = colorsConst;
    if (colorsParam && colorsParam.length) {
        colors = colorsParam;
    }
    if (originalGraphType != 'treemap') {
        var chart = Highcharts.chart(selector, {
            chart: {
                type: graphType,
                backgroundColor: 'rgba(0,0,0,0)',
                color: '#fff',
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                marginTop: 20,
                //innerSize: '50%' // Make it a donut chart
            },
            title: {
                text: title,
                align: 'left',
            },
            credits: {
                enabled: false
            },
            //colorAxis: colorAxis,
            legend: {
                useHTML: true,
                labelFormatter: function () {
                    if (this.name == "Female") {
                        if (this.legendItem.symbol.element) {
                            this.legendItem.symbol.element.outerHTML = "";
                        }

                        return `<span style='color:${this.color}; font-size: 12px; font-weight: 600;'> <i class='fa fa-female' aria-hidden='true' style="font-size: 14px;" ></i> ${this.name}</span>`;
                    } else if (this.name == "Male") {
                        if (this.legendItem.symbol.element) {
                            this.legendItem.symbol.element.outerHTML = "";
                        }
                        return `<span style='color:${this.color}; font-size: 12px; font-weight: 600;'> <i class='fa fa-male' aria-hidden='true' style="font-size: 14px;" ></i> ${this.name}</span>`;
                    } else {
                        return this.name;
                    }
                }
            },
            plotOptions: {
                series: {
                    stacking: originalGraphType == 'stack-bar' || originalGraphType == 'stack-column' ? 'normal' : null,
                    shadow: false,
                    borderRadius: '0',
                    borderWidth: 0,
                    dataLabels: {
                        enabled: !(originalGraphType == 'stack-column'),
                        format: '{point.y:.0f}',
                        //inside: !(originalGraphType == 'stack-column'),
                        //inside: originalGraphType == 'stack-bar' || originalGraphType != 'stack-column' , // Set this to false to place labels outside
                        crop: false, // Prevent labels from being cropped
                        //overflow: 'none', // Prevent labels from overflowing the chart area
                        format: '{point.y:.0f}',
                        style: {
                            fontSize: 12,
                            borderWidth: 0
                        }
                    },

                },
                pie: {
                    size: '80%',
                    allowPointSelect: true,
                    cursor: 'pointer',
                    crop: false,
                    overflow: 'none',
                    dataLabels: {
                        enabled: true,
                        crop: false,
                        overflow: 'none',
                        format: '{point.name}: {point.y}%',
                        distance: 10,

                    },
                    showInLegend: false
                },
                //column: {
                //    colorByPoint: graphType == 'column' ? true : null,
                //}
            },
            tooltip: {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                formatter: function () {
                    console.log(this);
                    const value = this.point.options && this.point.options.total
                        ? this.point.options.total
                        : this.point.y;

                    return `<span style="font-size:12px;color:${this.color}">${graphType == 'pie' ? "" : (this.series.name ? this.series.name : this.point.name)}</span><br/>` +
                        `<span style="color:${this.point.color}">${this.point.name ? this.point.name : this.x}</span>: ` +
                        `<b style="color:${this.color}">${value}</b><br/>`;
                }
            },
            colors: colors,
            xAxis: {
                categories: categories,
                labels: {
                },
            },
            yAxis: {
                visible: true,
                gridLineColor: 'transparent',
                labels: {
                    style: {
                        display: 'none'
                    },
                },
                title: {
                    text: yAxis,
                },
                stackLabels: {
                    enabled: true,
                    style: {
                        fontWeight: 'bold',
                        color: ( // theme
                            Highcharts.defaultOptions.title.style &&
                            Highcharts.defaultOptions.title.style.color
                        ) || 'gray'
                    },
                    overflow: 'none',
                    crop: false,
                    rotation: (originalGraphType == 'stack-column') ? 270 : 0,
                    y: (originalGraphType == 'stack-column') ? -8 : 0,
                    formatter: function () {
                        var sum = 0;
                        var series = this.axis.series;
                        series.forEach(serie => {
                            if (serie.visible && serie.options.stacking === 'normal') {
                                // Access the corresponding point for this x value
                                const point = serie.userOptions.data[this.x];
                                if (point) {
                                    sum += point.y || 0; // Add the y value of the point
                                }
                            }
                        });
                        if (this.total > 0) {
                            return parseInt(sum);
                        } else {
                            return '';
                        }
                    }
                }
            },
            series: series
        });
    }
    else if (originalGraphType == 'treemap') {
        Highcharts.chart(selector, {
            chart: {
                //inverted: true,
                alternateStartingDirection: true,
            },
            credits: {
                enabled: false
            },
            colorAxis: {
                visible: false,
                minColor: colorsConst[0],
                maxColor: colorsConst[1],
            },
            series: series,
            tooltip: {
                formatter: function () {
                    var str = `${this.point.name}: ${this.point.value}(${this.point.percentage}%)`,
                        node = this.point.node;

                    if (node) {
                        node.children.forEach(function (child) {
                            str += '<br />' + child.name + ' ' + child.val;
                        });
                    }
                    return str;
                }
            },
            title: {
                text: ''
            }
        });
    }
    //if (chart) {
    //    if (originalGraphType == 'treemap') {
    //        console.log(chart);
    //        //chart.colorAxis = {
    //        //    visible: originalGraphType == 'treemap',
    //        //    minColor: colors[0],
    //        //    maxColor: colors[1]//Highcharts.getOptions().colors[0]
    //        //};
    //        chart.colorAxis = colorsConst;
    //    }
    //}

}