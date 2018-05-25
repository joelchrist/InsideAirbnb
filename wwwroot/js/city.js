const CityPage = (($, mapBox) => {
    const STYLE = 'mapbox://styles/joelchrist/cjh91253t7h872rs4afm2l9jl';
    let _map = {};
    let _neighbourhood;
    let _popup;
    
    //// Public 
    
    
    let setup = async (neighbourhood) => {
        console.log(`Setting up the Mapbox for neighbourhood: ${neighbourhood}`);
        _neighbourhood = neighbourhood;
        popup = new mapboxgl.Popup({
            closeButton: false
        });
        mapBox.accessToken = 'pk.eyJ1Ijoiam9lbGNocmlzdCIsImEiOiJjamg5MHpud3UwMDN2MzBtcTljeHFsNHRsIn0.yN9COTx4KsT3AiwitMP_2g';
        _map = new mapBox.Map({
            container: 'map',
            style: STYLE,
        });

        _map.on('load', async () => {
            let response = await fetch(`/api/listings/filter?neighbourhood=${_neighbourhood}`);
            let listings = await response.json();
            let layer = await getLayer(listings);
            _map.addLayer(layer)
        });
    };
    
    //// Private

    let getLayer = async (listings) => {
        let features = listings.map(listing => {
            return {
                type: 'Feature',
                geometry: {
                    type: 'Point',
                    coordinates: [
                        (parseFloat(`${listing.longitude.toString().slice(0, 1)}.${listing.longitude.toString().slice(1)}`)),
                        (parseFloat(`${listing.latitude.toString().slice(0, 2)}.${listing.latitude.toString().slice(2)}`)),
                    ]
                },
                properties: {
                    title: listing.name,
                    instantBookable: listing.instantBookable,
                }
            };
        });
        
        return {
            id: 'listings',
            type: 'circle',
            source: { 
                type: 'geojson',
                data: {
                    type: 'FeatureCollection',
                    features,
                }
            },
            paint: {
                'circle-color': [
                    'match',
                    ['get', 'instantBookable'],
                    't', '#02E500',
                    'f', '#e55e5e',
                    '#000000'
                ]
            }
        }
    }
    
    ////////
    
    return {
        setup
    }
    
})(jQuery, mapboxgl);

