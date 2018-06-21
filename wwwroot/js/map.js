const MapLogic = (($, mapBox) => {
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
            let filter = FormLogic.asString('#filter');
            let listings = await getListings(filter);
            let source = getSource(listings);
            _map.addSource('listings_source', source);
            let layer = getLayer('listings_source');
            _map.addLayer(layer);
        }).on('click', 'listings', async event => {
            console.log("hoi daar");
            let popup = await getPopup(event.features[0].properties.id);
            popup.addTo(_map);
        });
    };
    
    let updateMap = async (e) => {
        e && e.preventDefault();
        let filter = FormLogic.asString('#filter');
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
                    id: listing.id,
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

    let getPopup = async id => {
        const response = await fetch(`/api/listings/${id}`);
        const data = await response.json();
        console.log(data);
        const html = `
                    <img src="${data.pictureUrl}" style="width:250px;">
                    <dl style='margin-bottom: 0;'>
                        <dt><a href='${data.hostUrl}'>${data.hostName}</a></dt>
                        <dd style='border-bottom: 1px #ccc solid;'>${data.hostListingsCount - 1 > 0 ? data.hostListingsCount - 1 : 'No'} other listing(s)</dd>
                        <dt><a href='${data.listingUrl}'>${data.name}</a></dt>
                        <dd>${data.neighbourhood === null ? 'N/A' : data.neighbourhood}</dd>
                        <dd style='border-bottom: 1px #ccc solid;'>${data.roomType}</dd>
                        <dd>${data.price} per night</dt>
                        <dd style='border-bottom: 1px #ccc solid;'>${data.minimumNight === undefined ? 0 : data.minimumNight} night(s) minimum</dd>
                        <dt>${data.calendar.filter(c => c.price !== null).length} night(s) per year</dt>
                        <dd>${data.numberOfReviews} review(s)</dt>
                        <dd>${data.reviewsPerMonth === null ? 0 : data.reviewsPerMonth / 100} review(s) per month</dd>
                        <dd>Last review: ${data.lastReview === null ? 'N/A' : data.lastReview.split('T', 1)}</dd>
                        <dd>${data.availability365} day(s) per year</dd>
                    </dl>`;
        return new mapBox
            .Popup()
            .setLngLat([
                parseFloat(`${data.longitude}`.split('').map((current, index) => index === 1 ? `.${current}` : current).join('')),
                parseFloat(`${data.latitude}`.split('').map((current, index) => index === 2 ? `.${current}` : current).join(''))
            ])
            .setHTML(html)
    };
    
    ////////
    
    return {
        setup,
        updateMap
    }
    
})(jQuery, mapboxgl);   