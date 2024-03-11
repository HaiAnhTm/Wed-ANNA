function actionPayBill ()
{
    $.ajax( {
        url: "/ConsumerProduct/ConsumerCart",
        type: "POST",
        data: {},
        success: function ( result )
        {
            console.log( result );
            if ( result.status )
            {
                toastr.options.closeButton = true;
                toastr.success( result.message, "Thanh toán" );
                location.reload();
            } else
            {
                //window.location.href = result.url;
                toastr.options.closeButton = true;
                toastr.warning( result.message, "Thanh toán" );
            }
        },
        error: function ()
        {
            console.error( "AJAX request failed" );
        },
    } );
}

function createFormPost ( data )
{
    var form = document.createElement( "form" );
    form.action = "/ConsumerProduct/ConsumerCart"
    form.method = "POST";
    for ( var key in data )
    {
        if ( data.hasOwnProperty( key ) )
        {
            var input = document.createElement( "input" );
            input.type = "hidden";
            input.name = key;
            input.value = data[ key ];
            form.appendChild( input );
        }
    }
    document.body.appendChild( form );
    form.submit();
}

function updateListCart ( idProduct, quantity )
{
    $.ajax( {
        url: "/ConsumerProduct/AddProductToCart",
        type: "POST",
        data: {
            productId: idProduct,
            quantity: quantity,
        },
        success: function ( result )
        {
            console.log( result );
            if ( result.status )
            {
                console.log( "Buy Success." );
                toastr.options.closeButton = true;
                toastr.success( "Giỏ hàng", "Thêm sản phẩm thành công!" );
                location.reload();
            } else
            {
                console.log( "Buy Failed." );
                window.location.href = result.url;
                toastr.options.closeButton = true;
            }
        },
        error: function ()
        {
            console.error( "AJAX request failed" );
        },
    } );
}


function appDiscount ()
{
    const inputCode = document.getElementById( "input-code" );

    var code = inputCode.value;

    if ( code.length > 0 )
    {
        $.ajax( {
            url: "/ConsumerProduct/AddDiscount",
            type: "POST",
            data: {
                codeDiscount: code,
            },
            success: function ( result )
            {
                console.log( result );
                if ( result.status )
                {
                    console.log( "Buy Success." );
                    toastr.options.closeButton = true;
                    toastr.warning( "Khuyến mại", "Áp mã giảm giá thành công" );
                    location.href = result.url;
                } else
                {
                    toastr.options.closeButton = true;
                    toastr.warning(
                        result.message ?? "Mã khuyến mãi không khả dụng",
                        "Khuyến mại"
                    );
                }
            },
        } );
    } else
        document.getElementById( "notify_GiamGia" ).textContent =
            "Nhập mã giảm giá trước khi áp dung.";
}

function actionPayBill ()
{
    $.ajax( {
        url: "/ConsumerProduct/PayBill",
        type: "POST",
        data: {},
        success: function ( result )
        {
            if ( result.status )
            {
                toastr.options.closeButton = true;
                toastr.success( result.message, "Giỏ hàng" );
                setTimeout( function ()
                {
                    location.reload();
                }, 500 );
            } else
            {
                toastr.options.closeButton = true;
                toastr.warning( result.message, "Giỏ hàng" );
            }
        },
        error: function ()
        {
            console.error( "AJAX request failed" );
        },
    } );
}
function actionCancelBill ()
{
    $.ajax( {
        url: "/ConsumerProduct/CancelBill",
        type: "POST",
        data: {},
        success: function ( result )
        {
            console.log( result );
            if ( result.status )
            {
                toastr.options.closeButton = true;
                toastr.success( result.message, "Thanh toán" );
                location.reload();
            } else
            {
                //window.location.href = result.url;
                toastr.options.closeButton = true;
                toastr.warning( result.message, "Thanh toán" );
            }
        },
        error: function ()
        {
            console.error( "AJAX request failed" );
        },
    } );
}


function deleteProduct (idProduct)
{
    $.ajax( {
        url: "/ConsumerProduct/RemoveFromCart",
        type: "POST",
        data: {
            productId: idProduct
        },
        success: function ( result )
        {
            console.log( result );
            if ( result.status )
            {
                toastr.options.closeButton = true;
                toastr.success( "Giỏ hàng", "Cập nhật giỏ hàng thành công!" );
                location.reload();
            } else
            {
                window.location.href = result.url;
                toastr.options.closeButton = true;
                toastr.warning( "Giỏ hàng", "Cập nhật giỏ hàng thất bại!" );
            }
        },
        error: function ( error )
        {
            console.error( "AJAX request failed", error );
        },
    } );
}