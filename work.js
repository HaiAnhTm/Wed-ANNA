function showProducts(products) {
   var productDetailId = document.getElementById('content-product-list');

   productDetailId.innerHTML = "";

   products.forEach((item) => {
       var htmlContent = `
            <div class="adProduct-content-body">
            <div class="col l-3 content-body-name">
               <img class="adProduct-body-product-img" src="${item.image_url}"
                     alt="">
               <div class="adProduct-product-name">${item.name_product}</div>
            </div>
      
            <div class="col l-2 adProduct-product">
               <div class="adProduct-product-manucafer">${item.description}</div>
            </div>
            <div class="col l-2 adProduct-product-unitprice adProduct-product">
               <div class="adProduct-product-manucafer">₫${item.price}</div>
            </div>
            <div class="col l-1 adProduct-product-unitprice adProduct-product">
               <div class="adProduct-product-manucafer">${item.discount_product}%</div>
            </div>
            <div class="col l-1 adProduct-product-unitprice adProduct-product">
               <div class="adProduct-product-manucafer">${item.quantity}</div>
            </div>
            <div class="col l-1 adProduct-product-quantity adProduct-product">
               <div class="adProduct-product-manucafer product-quantity">${item.type_product}</div>
            </div>
            <div class="col l-1 adProduct-product">
               <div class="adProduct-product-manucafer">${item.status_product}</div>
            </div>
            <div class="col l-1 adProduct-product">
               <a href="/UpdateProduct/${item.id_product}" class="product-Update-operation icon-update">Cập nhật</a>
               <div class="product-Update-operation" id="iconDelete" onclick="showDeleteDiaLogConfirm('${item.id_product}')">Xóa</div>
            </div>
         </div>
       `;
       productDetailId.insertAdjacentHTML('beforeend', htmlContent);
   });
}


// @foreach (var item in Model)
// {
//     <div class="adProduct-content-body">
//         <div class="col l-3 content-body-name">
//             <img class="adProduct-body-product-img" src="@item.Image" alt="">
//             <div class="adProduct-product-name">@Html.DisplayFor(modelItem => item.NameProduct)</div>
//         </div>

//         <div class="col l-2 adProduct-product">
//             <div class="adProduct-product-manucafer">@Html.DisplayFor(modelItem => item.Description)</div>
//         </div>
//         <div class="col l-2 adProduct-product-unitprice adProduct-product">
//             <div class="adProduct-product-manucafer">₫@Html.DisplayFor(modelItem => item.Price)</div>
//         </div>
//         <div class="col l-1 adProduct-product-unitprice adProduct-product">
//             <div class="adProduct-product-manucafer">@Html.DisplayFor(modelItem => item.Discount)%</div>
//         </div>
//         <div class="col l-1 adProduct-product-unitprice adProduct-product">
//             <div class="adProduct-product-manucafer">@Html.DisplayFor(modelItem => item.Quantity)</div>
//         </div>
//         <div class="col l-1 adProduct-product-quantity adProduct-product">
//             <div class="adProduct-product-manucafer product-quantity">@Html.DisplayFor(modelItem => item.TypeProduct.TypeProductName)</div>
//         </div>
//         <div class="col l-1 adProduct-product">
//             <div class="adProduct-product-manucafer">@Html.DisplayFor(model => item.TypeProductSale.StatusProduct)</div>
//         </div>
//         <div class="col l-1 adProduct-product">
//             @Html.ActionLink("Cập nhật", "UpdateProduct", new { id = item.IdProduct }, new { @class = "product-Update-operation icon-update " })

//             <div class="product-Update-operation" id="iconDelete" onclick="showDeleteDiaLogConfirm('@item.IdProduct')">Xóa</div>
//         </div>
//     </div>

//     <div class="footer-chose">
//         <div class="footer-chose-left">
//             <input type="checkbox" class="footer-chose-left-checkbox">
//             <div class="footer-chose-left-text">Chọn Tất cả</div>
//         </div>
//         <div class="footer-chose-right">
//             <div class="footer-chose-right-text">69 sản phẩm đã được chọn</div>
//             <button class="footer-chose-right-btn btn btn_delete" id="iconDelete" onclick="showDiaLogConfirm('@item.IdProduct')">@Html.ActionLink("Xóa", "DeleteProduct", new { id = item.IdProduct }, new { @class = "icon-delete" }) </button>
//         </div>
//     </div>
// }
