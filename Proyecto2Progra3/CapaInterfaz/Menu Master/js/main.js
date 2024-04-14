(function($) {

	"use strict";

	$('nav .dropdown').hover(function(){
		var $this = $(this);
		$this.addClass('show');
		$this.find('> a').attr('aria-expanded', true);
		$this.find('.dropdown-menu').addClass('show');
	}, function(){
		var $this = $(this);
			$this.removeClass('show');
			$this.find('> a').attr('aria-expanded', false);
			$this.find('.dropdown-menu').removeClass('show');
	});

})(jQuery);
jQuery(document).ready(function () {
	$(".oculto").hide();
	$(".inf").click(function () {
		var nodo = $(this).attr("href");

		if ($(nodo).is(":visible")) {
			$(nodo).hide();
			return false;
		} else {
			$(".oculto").hide("slow");
			$(nodo).fadeToggle("fast");
			return false;
		}
	});
}); 
