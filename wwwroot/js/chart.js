const ChartLogic = (($, Chart) => {
    ///// Public
    let _priceChart = {};
    
    let setup = async () => {
        // let filter = FormLogic.asString('#filter');
        // let result = await fetch(`api/history/price?${filter}`);
        // let body = await result.json();
        //
        // _priceChart = Chart('#priceChart', {
        //     type: 'bar',
        //     data: barChartData,
        //     options: {
        //         responsive: true,
        //         legend: {
        //             position: 'top',
        //         },
        //         title: {
        //             display: true,
        //             text: 'Chart.js Bar Chart'
        //         }
        //     },
        // });
        //
    };
    
    ///// Private
    
    
    let getChartData = () => {
        
    };
    
    
    
    ///// Public
    
    return {
        setup
    }
})(jQuery, Chart);