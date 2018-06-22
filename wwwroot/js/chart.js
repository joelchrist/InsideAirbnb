const ChartLogic = (($, Chart) => {
    ///// Public
    let _priceChart = {};
    let _availabilityChart = {};
    let _roomTypeChart = {};

    let setup = async () => {
        let stats = await getChartData();
        
        setupPriceChart(stats);
        setupAvailabilityChart(stats);
        setupRoomTypeChart(stats);

    };
    
    ///// Private
    
    let setupPriceChart = (stats) => {
        _priceChart = new Chart($('#priceChart')[0].getContext('2d'), {
            type: 'line',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'Semptember', 'October', 'November', 'December'],
                datasets: [{
                    label: 'Prices',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgb(255, 99, 132)',
                    data: [
                        stats.price[0],
                        stats.price[1],
                        stats.price[2],
                        stats.price[3],
                        stats.price[4],
                        stats.price[5],
                        stats.price[6],
                        stats.price[7],
                        stats.price[8],
                        stats.price[9],
                        stats.price[10],
                        stats.price[11],
                        stats.price[12],
                    ],
                    fill: false,
                }]
            },
            options: {
                responsive: true,
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'Average price per month'
                }
            },
        });
    };

    let setupAvailabilityChart = (stats) => {
        _availabilityChart = new Chart($('#availabilityChart')[0].getContext('2d'), {
            type: 'horizontalBar',
            data: {
                labels: ['High', 'Low'],
                datasets: [
                    {
                        label: 'Availability',
                        data: [
                            stats.availability.high, 
                            stats.availability.low,
                        ],
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                        ],
                        borderColor: [
                            'rgb(255, 99, 132)',
                            'rgb(75, 192, 192)',
                        ]
                    }
                ]
            },
            options: {
                responsive: true,
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'Availability'
                }
            }
        });
    };
    
    let setupRoomTypeChart = (stats) => {
        _roomTypeChart = new Chart($('#roomTypeChart')[0].getContext('2d'), {
            type: 'pie',
            data: {
                labels: ['Private Room', 'Shared room', 'Entire house/apt'],
                datasets: [{
                    label: 'Prices',
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(54, 162, 235, 0.2)'
                    ],
                    borderColor: [
                        'rgb(255, 99, 132)',
                        'rgb(75, 192, 192)',
                        'rgb(54, 162, 235)'
                    ],
                    data: [
                        stats.roomType.privateRooms,
                        stats.roomType.sharedRooms,
                        stats.roomType.entireHomes
                    ],
                    fill: false,
                }]
            },
            options: {
            }
            
        })
    };
    
    
    let getChartData = async () => {
        let filter = FormLogic.asString('#filter');
        let response = await fetch(`api/stats?${filter}`);
        return await response.json();
    };
    
    
    
    ///// Public
    
    return {
        setup,
    }
})(jQuery, Chart);