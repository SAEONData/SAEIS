namespace Info {


    // Map

    export class MapPoint {
        name: string;
        latitude: number;
        longitude: number;
        constructor(name: string, latitude: number, longitude: number) {
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }

    let map: google.maps.Map;
    let mapPoint: MapPoint;
    
    export function InitMap() {
        let mapOpts: google.maps.MapOptions = {
            center: new google.maps.LatLng(mapPoint.latitude, mapPoint.longitude),
            zoom: 15
        };
        map = new google.maps.Map(document.getElementById('mapLocations'), mapOpts);
        let marker = new google.maps.Marker({
            position: { lat: mapPoint.latitude, lng: mapPoint.longitude },
            map: map,
            title: mapPoint.name
        });
        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
    }

    export function SetMapPoint(name: string, latitude: number, longitude: number) {
        mapPoint = new MapPoint(name, latitude, longitude);
    }

}