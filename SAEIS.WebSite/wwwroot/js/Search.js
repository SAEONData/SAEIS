var Search;
(function (Search) {
    // Errors
    function ShowWaiting() {
        $("#modalLoading").modal("show");
    }
    Search.ShowWaiting = ShowWaiting;
    function HideWaiting() {
        $("#modalLoading").modal("hide");
    }
    Search.HideWaiting = HideWaiting;
    function ErrorInFunc(method, status, error) {
        HideWaiting();
        alert("Error in " + method + " Status: " + status + " Error: " + error);
    }
    function DrawEstuariesTable(filters) {
        if (!filters) {
            filters = GetFilters();
        }
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);
        function drawTable() {
            $.post("/Search/GetEstuaries", filters)
                .done(function (json) {
                var data = new google.visualization.DataTable();
                data.addColumn('number', '#');
                data.addColumn('string', 'Name');
                data.addColumn('string', 'Province');
                data.addColumn('string', 'Classification');
                var items = json;
                for (var i = 0; i < items.length; i++) {
                    data.addRow([items[i].id, items[i].name, items[i].province, items[i].classification]);
                }
                var table = new google.visualization.Table(document.getElementById('tableEstuaries'));
                table.draw(data, { allowHtml: true, width: '100%', height: '100%', page: 'enable', pageSize: 25 });
            })
                .fail(function (jqXHR, status, error) {
                ErrorInFunc("GetEstuaries", status, error);
            });
        }
    }
    var map;
    var markers = [];
    var mapPoints;
    var mapBounds;
    var mapFitted = false;
    function InitMap() {
        var mapOpts = {
            center: new google.maps.LatLng(-30.913054, 24.669581),
            zoom: 6,
            mapTypeId: google.maps.MapTypeId.SATELLITE
        };
        map = new google.maps.Map(document.getElementById('mapLocations'), mapOpts);
        UpdateMap(GetFilters());
        FitMap();
    }
    Search.InitMap = InitMap;
    function UpdateMap(filters) {
        if (!filters) {
            filters = GetFilters();
        }
        $.post("/Search/GetMapData", filters)
            .done(function (json) {
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];
            mapPoints = json;
            mapBounds = new google.maps.LatLngBounds();
            var _loop_1 = function (i) {
                var mapPoint = mapPoints[i];
                var marker = new google.maps.Marker({
                    position: { lat: mapPoint.latitude, lng: mapPoint.longitude },
                    map: map,
                    title: mapPoint.name
                });
                markers.push(marker);
                mapBounds.extend(marker.getPosition());
                marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
                marker.addListener('click', function () {
                    window.location.href = mapPoint.url;
                });
            };
            for (var i = 0; i < mapPoints.length; i++) {
                _loop_1(i);
            }
        })
            .fail(function (jqXHR, status, error) {
            ErrorInFunc("UpdateMap", status, error);
        });
    }
    function FitMap(override) {
        if (override === void 0) { override = false; }
        if (override || (!mapFitted && (mapBounds != null) && !mapBounds.isEmpty())) {
            map.setCenter(mapBounds.getCenter());
            map.fitBounds(mapBounds);
            mapFitted = true;
        }
    }
    Search.FitMap = FitMap;
    function FixMap() {
        UpdateMap(GetFilters());
        FitMap(true);
        alert(map.getCenter() + " " + map.getZoom());
    }
    Search.FixMap = FixMap;
    // Filter updates
    var Filters = /** @class */ (function () {
        function Filters() {
        }
        return Filters;
    }());
    function GetFilters() {
        var filters = new Filters();
        var name = $("#Name").val();
        if (name != "") {
            filters.name = name;
        }
        var classification = $("#Classification").val();
        if (classification) {
            filters.classification = classification;
        }
        var region = $("#Region").val();
        if (region) {
            filters.region = region;
        }
        var condition = $("#Condition").val();
        if (condition) {
            filters.condition = condition;
        }
        var province = $("#Province").val();
        if (province) {
            filters.province = province;
        }
        return filters;
    }
    function UpdateFilters() {
        //ShowWaiting();
        var filters = GetFilters();
        UpdateMap(filters);
        DrawEstuariesTable(filters);
        //setTimeout(HideWaiting(),2000);
    }
    Search.UpdateFilters = UpdateFilters;
})(Search || (Search = {}));
//# sourceMappingURL=Search.js.map