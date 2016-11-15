 jQuery(document).ready(function($){
$(".all-portfolios").isotope({
itemSelector : '.single-portfolio',

layoutMode: 'fitRows',
});
$('ul.filter li').click(function(){
$("ul.filter li").removeClass("active");
$(this).addClass("active");
 
var selector = $(this).attr('data-filter');
$(".all-portfolios").isotope({
	
filter: selector,
animationOptions: {
duration: 750,
easing: 'linear',
queue: false,
}
});
return false;
});
});




jQuery(document).ready(function () {

	
/*----------------------------------------------------*/
/*  Animated Progress Bars
/*----------------------------------------------------*/

    jQuery('.skills li').each(function () {
        jQuery(this).appear(function() {
          jQuery(this).animate({opacity:1,left:"0px"},800);
          var b = jQuery(this).find(".progress-bar").attr("data-width");
          jQuery(this).find(".progress-bar").animate({
            width: b + "%"
          }, 1300, "linear");
        }); 
    });   

/*----------------------------------------------------*/
/* Crousel Team 
/*----------------------------------------------------*/
	$('.all_team').owlCarousel({
		items:5,
		loop:true,
		margin:5,
		nav:true,
		autoplay:false,
		smartSpeed:3000,
		navText: ["",""],
		responsive:{
			0:{
				items:1
			},
			600:{
				items:1
			},
			1000:{
				items:5
			}
		}
	})
	
/*preloder*/
$(window).load(function() { // makes sure the whole site is loaded
	$('#status').fadeOut(); // will first fade out the loading animation
	$('#loader-wrapper').delay(200).fadeOut('slow'); // will fade out the white DIV that covers the website.
	$('body').delay(200).css({'overflow-x':'hidden'});
})	
	
	
	
});

