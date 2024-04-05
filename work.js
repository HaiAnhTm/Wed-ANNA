function showProducts(products) {
  var productDetailId = document.getElementById("content-discount-list");

  productDetailId.innerHTML = "";

  products.forEach((item) => {
    var htmlContent = 
    `
    <div class="addiscount-content-body ongoing">

      <div class="col l-4 content-body-name">
        <img class="addiscount-body-discount-img" src="${item.image_url}" alt="" />

        <div class="discount_name_and_code">

          <div class="addiscount-discount">${item.name_discount}</div>

          <div class="addiscount-discount-code">
            Mã Voucher: ${item.code_discount}
          </div>
          
        </div>
      </div>

      <div class="col l-1 addiscount-discount">
        <div class="addiscount-discount">${item.percent} %</div>
      </div>

      <div class="col l-1 addiscount-discount-unitprice addiscount-discount">
        <div class="addiscount-discount">${item.quantity}</div>
      </div>

      <div class="col l-2 addiscount-discount-quantity addiscount-discount">
        <div class="addiscount-discount">${item.status}</div>
      </div>

      <div class="col l-3 addiscount-discount">
        <div class="addiscount-discount">${item.duration}</div>
      </div>

      <div class="col l-2 addiscount-discount">
        <div class="discount-Update-operation">Cập nhật</div>
        <div class="discount-Update-operation">Xóa</div>
        <div class="discount-Update-operation">Xem Thêm</div>
      </div>
    </div>
    `;
    productDetailId.insertAdjacentHTML("beforeend", htmlContent);
  });
}