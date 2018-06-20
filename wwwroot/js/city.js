const CityPage = (($, mapBox) => {
    const STYLE = 'mapbox://styles/joelchrist/cjh91253t7h872rs4afm2l9jl';
    let _map = {};
    
    //// Public 
    
    
    let setup = async () => {
        mapBox.accessToken = 'pk.eyJ1Ijoiam9lbGNocmlzdCIsImEiOiJjamg5MHpud3UwMDN2MzBtcTljeHFsNHRsIn0.yN9COTx4KsT3AiwitMP_2g';
        _map = new mapBox.Map({
            container: 'map',
            style: STYLE,
        });

        _map.on('load', async () => {
            let filter = $('#filter').serialize();
            let listings = await getListings(filter);
            let source = getSource(listings);
            _map.addSource('listings_source', source);
            let layer = getLayer('listings_source');
            _map.addLayer(layer);
        });
    };
    
    let updateMap = async (e) => {
        e && e.preventDefault();
        let filter = $('#filter').serialize();
        let listings = await getListings(filter);
        let source = getSource(listings);
        _map.getSource('listings_source').setData(source.data);
    };
    
    //// Private
    
    let getListings = async (filter) => {
        let url = `/api/listings/filter?${filter}`;
        let response = await fetch(url);
        return await response.json();
    };

    let getSource = (listings) => {
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
            type: 'geojson',
                data: {
                type: 'FeatureCollection',
                features,
            }
        };
    }
    
    let getLayer = (source) => {
        return {
            id: 'listings',
            type: 'circle',
            source, 
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
    };
    
    ////////
    
    return {
        setup,
        updateMap
    }
    
})(jQuery, mapboxgl);

