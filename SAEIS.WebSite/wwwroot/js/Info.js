var Info;
(function (Info) {
    // Map
    var MapPoint = /** @class */ (function () {
        function MapPoint(name, latitude, longitude) {
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
        }
        return MapPoint;
    }());
    Info.MapPoint = MapPoint;
    var map;
    var mapPoint;
    function InitMap() {
        var mapOpts = {
            center: new google.maps.LatLng(mapPoint.latitude, mapPoint.longitude),
            zoom: 15
        };
        map = new google.maps.Map(document.getElementById('mapLocations'), mapOpts);
        var marker = new google.maps.Marker({
            position: { lat: mapPoint.latitude, lng: mapPoint.longitude },
            map: map,
            title: mapPoint.name
        });
        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
    }
    Info.InitMap = InitMap;
    function SetMapPoint(name, latitude, longitude) {
        mapPoint = new MapPoint(name, latitude, longitude);
    }
    Info.SetMapPoint = SetMapPoint;
})(Info || (Info = {}));
//# sourceMappingURL=Info.js.map