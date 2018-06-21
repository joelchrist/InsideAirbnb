FormLogic = (($) => {
    
    ///// Public 

    let asString = (id) => {
        return $(id).serialize();
    };
    
    return {
        asString
    }
    
})(jQuery);