function Load_DistrictMap(json_data, Pc, Den, ValType, IndType, paraextra) {
    const report = document.getElementById('reporttooltip');
    var jsonObject = JSON.parse(json_data);
    var reportMapDivWidth = $("#containermap").width();
    var tooltipX = (reportMapDivWidth / 2) + 25;
    // var MapHeight = reportMapDivWidth > 650 ? 500 : null;
    var MapHeight = reportMapDivWidth > 400 ? 300 : null;
    debugger;
    Highcharts.mapChart('containermap', {

        chart: {
            borderWidth: 1,
            borderColor: '#00000',
            height: MapHeight
        },

        title: {
            text: '<span style="font-size:13px;font-weight:bold;">District Map</span>'
        },
        subtitle: {
            text: '<span style="font-size:13px;"> <span  style="color: red; font-weight:bold; font-size:12px;"> ' + jsonObject[0].district + '* </span></span>'
        },
        //subtitle: {
        //    text: '<span style="font-size:13px;">(State Level Estimate :' + ' <span  style="color: red; font-weight:bold; font-size:12px;"> ' + Pc + ValType + ' ( Den = ' + Den + ')  </span>)</span>'
        //},
        credits: {
            enabled: false
        },
        legend: {
            enabled: true,
            align: 'center',
            //y: 60
        },
        mapNavigation: {
            enabled: false
        },
        tooltip: {
            shared: true
        },
        //tooltip: {
        //    backgroundColor: 'none',
        //    borderWidth: 0,
        //    shadow: false,
        //    useHTML: true,
        //    padding: 0,
        //    //headerFormat : '',
        //    pointFormatter: function () {
        //        pointFormat: return '<span style="font-size:12px;">' + this.NAME_3 + ' : ' + this.value + '</span>';
        //    },
        //    positioner: function () {
        //        return { x: tooltipX, y: 70 };
        //    }
        //},
        //colorAxis: {

        //    dataClasses: [{
        //        from: 0,
        //        to: 100,
        //        color: IndType > 0 ? '#ff8080' : '#00bb00',
        //        color: '#d5d5ff',

        //    }, {
        //        from: 100,
        //        to: 100,
        //        color: '#d5d5ff'
        //    }
        //]
        //},


        //plotOptions: {
        //    series: {
        //        point: {
        //            events: {
        //                click: function (e) {
        //                    debugger;
        //                    //if (paraextra == 1) {
        //                    //    //BindDataDistrict(this.DISTRICT);
        //                    //    console.log(location.href = '/Attendance/DailyAttendance?DistrictId=' + this.DISTRICT);
        //                    //    location.href = '/Attendance/DailyAttendance?DistrictId=' + this.DISTRICT
        //                    //        //this.options.key;
        //                    //}
        //                },
        //                mouseOver: function () {
        //                    report.innerHTML = 'Moused over';
        //                    report.style.color = 'green';
        //                },
        //                mouseOut: function () {
        //                    report.innerHTML = 'Moused out';
        //                    report.style.color = 'red';
        //                }
        //            }
        //        }
        //    }
        //},
        //plotOptions: {
        //    series: {
        //        events: {
        //            mouseOver: function () {
        //                report.innerHTML = 'Moused over';
        //                report.style.color = 'green';
        //            },
        //            mouseOut: function (e) {
        //                report.innerHTML = 'Moused out';
        //                report.style.color = 'red';
        //            }
        //        }
        //    }
        //},
        plotOptions: {
            series: {
                point: {
                    events: {
                        click: function () {
                            // alert('jhghn');
                            //alert($('#hdRoundType').val());
                            window.location.href = document.baseURI + "/Home/Blockmap?B=2&&R=1";// + $('#hdRoundType').val();
                        }
                    }
                }
            }
        },

        series: [{
            animation: {
                duration: 1000
            },
            data: jsonObject,
           
            mapData: Highcharts.maps['custom/DistOdisha'],
            joinBy: ['laa', 'district'],
            dataLabels: {
                enabled: true,
                color: '#000000',
                format: '{point.district}',
                style: {
                    fontSize: 9,
                    fontWeight: 600
                },
            },

            name: 'Odisha', //jsonObject[0].Section,
            //color: '#fff',
            borderColor: 'black',
            borderWidth: 0.5,
            tooltip: {
                pointFormat: '{point.district}'
            }
        }]
    });
}