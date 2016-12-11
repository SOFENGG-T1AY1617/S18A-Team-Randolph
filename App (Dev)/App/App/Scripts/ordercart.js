function order(docuID, deliveryRate, packaging, quantity, degree, price){
  this.docuID = docuID;
  this.deliveryRate = deliveryRate;
  this.packaging = packaging;
  this.quantity = quantity;
  this.degree = degree;
  this.price = price;
}
function addCart(docuID, deliveryRate, packaging, quantity, degree, price){
  var newOrder = new order(docuID, deliveryRate, packaging, quantity, degree, price);
  alert("trying to add to cart");
  if(sessionStorage.getItem("cart") === null){
      var cart = [];
      cart.push(newOrder);
      sessionStorage.cart = JSON.stringify(cart);
  }else{
    var cart = JSON.parse(sessionStorage.cart);
    cart.push(newOrder);
    sessionStorage.cart = JSON.stringify(cart);
  }
  alert(sessionStorage.cart);
}

function removeCart(cartIndex){
  var cart = JSON.parse(sessionStorage.cart);
  cart[cartIndex].splice(cartIndex,1);
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
  if((sessionStorage.getItem("cart") != null)){
    var docs = JSON.parse(sessionStorage.docs);
    var cart = JSON.parse(sessionStorage.cart);

    $('#viewCart').empty();
    for(var i=0; i<cart.length(); i++){
        var docuName = docs[cart[i].docuID].docuName;
        var price = cart[i].price * cart[i].quantity;
        $('#viewCart').append('<li>'+docuName+' - '+price+ ' - '+cart[i].quantity+'</li>');
    }
  }
}
