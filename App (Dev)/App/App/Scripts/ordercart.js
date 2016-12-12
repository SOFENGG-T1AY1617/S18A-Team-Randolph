function order(docuID, docuName, deliveryRate, packaging, quantity, degree, price){
  this.docuID = docuID;
  this.docuName = docuName;
  this.deliveryRate = deliveryRate;
  this.packaging = packaging;
  this.quantity = quantity;
  this.degree = degree;
  this.price = price;
}
function addCart(docuID, docuName, deliveryRate, packaging, quantity, degree, price){
  var newOrder = new order(docuID, docuName, deliveryRate, packaging, quantity, degree, price);
  if(sessionStorage.getItem("cart") === null){
      var cart = [];
      cart.push(newOrder);
      sessionStorage.cart = JSON.stringify(cart);
  }else{
    var cart = JSON.parse(sessionStorage.cart);
    cart.push(newOrder);
    sessionStorage.cart = JSON.stringify(cart);
  }
}

function removeCart(itemID){
  var cart = JSON.parse(sessionStorage.cart);

  for(var i=0; i<cart.length; i++){
    if(cart[i].docuID == itemID){
        cart.splice(i,1);
    }

  }
  sessionStorage.cart = JSON.stringify(cart);
}

function minusOrder(cartIndex){
  var cart = JSON.parse(sessionStorage.cart);
  if(cart[cartIndex].quantity == 1){
    cart[cartIndex].splice(cartIndex,1);
  }else{
    cart[cartIndex].quantity -= 1;
  }
  sessionStorage.cart = JSON.stringify(cart);
}
function addOrder(cartIndex){
  var cart = JSON.parse(sessionStorage.cart);
  if(cart[cartIndex].quantity == 5){
    window.alert("You cannot order more than 5")
  }else{
    cart[cartIndex].quantity -= 1;
  }
  sessionStorage.cart = JSON.stringify(cart);
}
function viewCart(){
    var cart = JSON.parse(sessionStorage.cart);
    $('#viewCart').empty();
    $('#viewCart').append('<table id="cartTable"class="table table-hover"><thead><tr><th>Product</th><th>Quantity</th>'+
    '<th class="text-center">Price</th><th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th></tr></thead></table>');
    for(var i=0; i<cart.length; i++){
        var docuName = cart[i].docuName;
        var price = cart[i].price * cart[i].quantity;
        $('#cartTable').append('<tr>'+
            '<td>&nbsp;&nbsp;&nbsp;'+docuName+'</td>'+
            '<td>'+cart[i].quantity+'</td>'+
            '<td> Php '+ price +'</td>'+
            '<td>'+
            '    <button type="button" class="btn btn-danger">'+
            '        <span class="glyphicon glyphicon-remove" id="removeCart" onclick="removeCart('+cart[i].docuID+')"></span>'+
            '    </button></td></tr>');
    }
}
