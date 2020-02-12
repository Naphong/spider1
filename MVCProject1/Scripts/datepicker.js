$(document).ready(function () {
        $('input').filter('.datepicker').datepicker({
            dateFormat: 'dd-M-y',
            showOtherMonths: true,
            
	    selectOtherMonths: true
        });
    });