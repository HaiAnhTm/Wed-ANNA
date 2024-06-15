const itemProductId = document.querySelectorAll(".item-container");

itemProductId.forEach((itemContainer) => {
    itemContainer.addEventListener("click", function (event) {
    const target = event.target;
    const itemId = itemContainer.dataset.itemId;
    if (target.classList.contains("add-to-cart-btn"))
        actionAddProductToCart(itemId, "1");
    else navigateToDetailProduct(itemId);
});
});

function navigateToDetailProduct(productId) {
    location.href = `/ConsumerProduct/DetailProduct?productID=${productId}`;
}

function actionAddProductToCart(productId, quantity) {
    addProductToCart(
    (productId = productId),
    (quantity = quantity),
    (onSuccess = function () {}),
    (onFailure = function () {})
);}

function addProductToCart(productId, quantity, onSuccess, onFailure) {
    $.ajax({
        url: "/ConsumerProduct/AddProductToCart",
        type: "POST",
        data: {
        productId: productId,
        quantity: quantity,
        },
        success: function (result) {
        console.log("Result: ", result);
        if (result.status) {
            toastr.options.closeButton = true;
            // Sau khi thông báo sẽ chờ khoảng 1s thì trang sẽ được load lại
            toastr.success(result.message, "Giỏ hàng");
            setTimeout(function () {
            location.reload();
        }, 600);
        } else {
            toastr.warning(result.message, "Giỏ hàng");
            setTimeout(function () {
            if (result.url !== null) {
                window.location.href = result.url;
            }
            }, 600);
        }
        },
        error: function () {
        console.error("AJAX request failed");
        },
    });
}
