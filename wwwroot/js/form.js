FormLogic = (($) => {
    
    let _listeners = [];
    
    console.log("bitch dafuq?");
    $('#filter').submit( e => {
        _listeners.forEach(l => l());
        e.preventDefault();
    });
    
    
    ///// Public 

    let asString = (id) => {
        return $(id).serialize();
    };
    
    let addSubmitListener = (callback) => {
        _listeners.push(callback);
    };
    
    return {
        asString,
        addSubmitListener
    }
    
})(jQuery);