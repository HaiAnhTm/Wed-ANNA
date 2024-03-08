function TongKetDate(doanhThu, sanPham) {
    document.getElementById('doanhThuTotal').textContent = formatCurrency(doanhThu);
    document.getElementById('sanPhamTotal').textContent = sanPham + " sản phẩm";
}

function formatCurrency(number) {
    const formattedCurrency = number.toLocaleString('vi-VN', {
        style: 'currency',
        currency: 'VND',
    });
    return formattedCurrency;
}

function ThongKeDateOnChange() {
    const dateInput = document.getElementById('thongKeDate').value;
    $.ajax({
        url: '/DashBoard/GetThongKe',
        type: 'POST',
        data: { date: dateInput },
        success: function (result) {
            TongKetDate(result.DoanhThu, result.SanPham);
            if (result.status) {
                console.log("Success");
            } else {
                console.log("False");
            }
        }
    });
}

function ThongKeDoanhThu(data) {
    var dataObject = JSON.parse(data);
    console.log("Data doanh thu: ", dataObject);
    const ctx = document.getElementById('doanhThuChart');
    const existingChart = Chart.getChart(ctx);
    if (existingChart) {
        existingChart.destroy();
    }
    new Chart(ctx, {
        data: {
            labels: dataObject.Ngay,
            datasets: [
                {
                    label: 'Doanh thu',
                    data: dataObject.DoanhThu,
                    borderWidth: 1,
                    type: 'line',
                    yAxisID: 'doanhThuYAxis'
                },
                {
                    label: 'SanPham',
                    data: dataObject.SanPham,
                    type: 'bar',
                    yAxisID: 'sanPhamYAxis'
                }
            ]
        },
        options: {
            scales: {
                sanPhamYAxis: {
                    display: false,
                }
            }
        }
    });
}



function ThongKeSanPham(data) {
    var dataObject = JSON.parse(data);
    console.log("Data san phẩm: ", dataObject);
    const ctx = document.getElementById('sanPhamChart');
    const existingChart = Chart.getChart(ctx);
    if (existingChart)
        existingChart.destroy();
    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: dataObject.TenSanPham,
            datasets: [{
                label: 'Sản phẩm',
                data: dataObject.SoLuong,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    display: false,
                }
            }
        }
    });
}
function ThongKeLineChart() {
    const dateStart = document.getElementById('dateStart').value;
    const dateEnd = document.getElementById('dateEnd').value;
    if (Date.parse(dateStart) > Date.parse(dateEnd)) {
        toastr.warning('Thống kê', 'Ngày bắt đầu lớn hơn ngày kết thúc!');
        return;
    }

    $.ajax({
        url:  '/DashBoard/GetChart',
        type: 'POST',
        data: { dateStart: dateStart, dateEnd: dateEnd },
        success: function (result) {
            if (result.status) {
                ThongKeDoanhThu(result.data);
                ThongKeSanPham(result.sanPham)
            } else {
                console.log("False");
            }
        }
    });
}
function ChartDateStartOnChange() {
    const dateEnd = document.getElementById('dateEnd');
    dateEnd.removeAttribute("disabled");
}
function ChartDateEndOnChange() {
    ThongKeLineChart();
}

function DefinePage() {
    const dateThongKe = document.getElementById('thongKeDate');
    const dateStart = document.getElementById('dateStart');
    const dateEnd = document.getElementById('dateEnd');

    var currentDate = new Date();
    var formattedDate = currentDate.toJSON().slice(0, 10);
    dateThongKe.value = formattedDate;
    dateEnd.value = formattedDate;

    currentDate.setDate(currentDate.getDate() - 7);
    formattedDate = currentDate.toISOString().split('T')[0];
    dateStart.value = formattedDate;

    ThongKeDateOnChange();
    ThongKeLineChart();
}
DefinePage();