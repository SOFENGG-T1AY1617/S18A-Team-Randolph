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
      return true;
  }else{
    var cart = JSON.parse(sessionStorage.cart);
    var isNew = true;
    for(var i=0; i<cart.length; i++){
      if(docuID == cart[i].docuID && degree == cart[i].degree){
        isNew = false;
        tempQuantity = parseInt(cart[i].quantity)  + parseInt(quantity);
        if(tempQuantity <=5){
          cart[i].quantity = tempQuantity;
          sessionStorage.cart = JSON.stringify(cart);
          return true;
        }else{
          alert("You cannot order more than 5 of the same document!");
          return false;
        }
      }
    }
    if(isNew){
      cart.push(newOrder);
      sessionStorage.cart = JSON.stringify(cart);
      return true;
    }
  }
}

function removeCart(itemID, degree){
  var cart = JSON.parse(sessionStorage.cart);
  for(var i=0; i<cart.length; i++){
    if(cart[i].docuID == itemID && cart[i].degree == degree){
        cart.splice(i,1);
    }

  }
  sessionStorage.cart = JSON.stringify(cart);
}
function removeCart(itemID, degree){
  var cart = JSON.parse(sessionStorage.cart);
  for(var i=0; i<cart.length; i++){
    if(cart[i].docuID == itemID && cart[i].degree == degree){
        cart.splice(i,1);
    }

  }
  sessionStorage.cart = JSON.stringify(cart);
}

function reloadRemoveCart(itemID, degree){
  var cart = JSON.parse(sessionStorage.cart);
  for(var i=0; i<cart.length; i++){
    if(cart[i].docuID == itemID && cart[i].degree == degree){
        cart.splice(i,1);
    }
    window.location.reload()
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

function editQuantity(docuID,degree,newQuantity){
  if(sessionStorage.cart != null){
    var cart = JSON.parse(sessionStorage.cart);

  }
}
function changeQuantity(input){
  var tempQuantity = $(input).val()
    if(tempQuantity > 5 || tempQuantity <=0){
      alert("Only a maximum of 5 is allowed");
    }
    $(input).val(5);
}
function viewCart(){
    $('#viewCart').empty();
    $('#viewCart').append('<table id="cartTable"class="table table-hover"><thead><tr><th>Product</th><th>Quantity</th>'+
    '<th class="text-center">Price</th><th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th></tr></thead></table>');
    if(sessionStorage.cart != null){
    var cart = JSON.parse(sessionStorage.cart);
    for(var i=0; i<cart.length; i++){
        var docuName = cart[i].docuName+"("+cart[i].degree+")";
        var price = cart[i].price * cart[i].quantity;
        $('#cartTable').append('<tr>'+
            '<td>&nbsp;&nbsp;&nbsp;'+docuName+'</td>'+
            '<td>'+cart[i].quantity+'</td>'+
            '<td> Php '+ price +'</td>'+
            '<td>'+
            '    <button type="button" class="btn btn-danger">'+
            '        <span class="glyphicon glyphicon-remove" id="removeCart" onclick="removeCart('+cart[i].docuID+', \''+cart[i].degree+'\');"></span>'+
            '    </button></td></tr>');
    }
  }
}
function populateEditCart(){
  var cart = JSON.parse(sessionStorage.cart);
  $('#editCartTable').empty();
  for(var i=0; i<cart.length; i++){
    $('#editCartTable').append('<tr>'+
                                '<td class="col-sm-8 col-md-6">'+
                                    '<div class="media">'+
                                        '<div class="media-body">'+
                                            '<h4 class="media-heading" style="color:#00703c;">'+cart[i].docuName+'</h4>'+
                                            '<h5 class="media-heading" style="color:#00703c;"> degree: '+cart[i].degree+'</h5>'+
                                        '</div>'+
                                    '</div>'+
                                '</td>'+
                                '<td class="col-sm-1 col-md-1" style="text-align: center">'+
                                    '<input type="number" class="form-control"onchange="changeQuantity(this);"onkeyup="this.onchange();" onpaste="this.onchange();" oninput="this.onchange(); id="cartQuantity" value="'+cart[i].quantity+'" max="5" min="1">'+
                                '</td>'+
                                '<td class="col-sm-1 col-md-1 text-center"><strong>P'+cart[i].price+'</strong></td>'+
                                '<td class="col-sm-1 col-md-1 text-center"><strong>P'+cart[i].price * cart[i].quantity+'</strong></td>'+
                                '<td class="col-sm-1 col-md-1">'+
                                    '<button type="button" class="btn btn-danger" class = "removeFromCart" onclick="reloadRemoveCart('+cart[i].docuID+',\''+cart[i].degree+'\')">'+
                                        '<span class="glyphicon glyphicon-remove"></span> Remove'+
                                    '</button></td></tr>');
  }
}
