document.addEventListener('DOMContentLoaded', function () {
    document.addEventListener('click', function (event) {
        // Xử lý cho việc toggle các mục con trong mỗi danh mục quản lý
        if (event.target.closest('.category-order-management')) {
            const categoryId = event.target.closest('.category-order-management').id;
            toggleSubItems(categoryId);
        }

        // Khi một mục quản lý cụ thể được chọn (clicked)
        if (event.target.classList.contains('order-management-items')) {
            setActive(event);
        }
    });
});

function toggleSubItems(categoryId) {
    const category = document.getElementById(categoryId);

    if (!category) return;

    const subItems = category.querySelectorAll('.order-management-items');
    const icon = category.querySelector('.chevron-manage-icon');

    // Toggle trạng thái hiển thị của các mục con và icon
    subItems.forEach(item => {
        item.classList.toggle('hidden');
    });

    if (icon) {
        icon.classList.toggle('fa-chevron-down');
        icon.classList.toggle('fa-chevron-up');
    }
}

function setActive(event) {
    const currentActive = document.querySelector('.order-management-items-active');

    // Nếu có một mục nào đó đã được chọn trước đó, bỏ chọn nó
    if (currentActive) {
        currentActive.classList.remove('order-management-items-active');
    }

    // Đặt mục được click thành mục đang hoạt động
    event.target.classList.add('order-management-items-active');
}
