namespace Search {
    // Errors

    export function ShowWaiting() {
        $("#modalLoading").modal("show");
    }

    export function HideWaiting() {
        $("#modalLoading").modal("hide");
    }

    function ErrorInFunc(method: string, status: string, error: string) {
        HideWaiting();
        alert("Error in " + method + " Status: " + status + " Error: " + error);
    }

    // Table

    interface EstuaryItem {
        id: number;
        name: string;
        province: string;
        classification: string;
    }

    function DrawEstuariesTable(filters: Filters) {
        if (!filters) {
            filters = GetFilters();
        }

        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            $.post("/Search/GetEstuaries",filters)
                .done(function (json) {
                    var data = new google.visualization.DataTable();
                    data.addColumn('number', '#');
                    data.addColumn('string', 'Name');
                    data.addColumn('string', 'Province');
                    data.addColumn('string', 'Classification');

                    let items: EstuaryItem[] = json;
                    for (let i = 0; i < items.length; i++) {
                        data.addRow([items[i].id, items[i].name, items[i].province, items[i].classification]);
                    }
                    var table = new google.visualization.Table(document.getElementById('tableEstuaries'));
                    table.draw(data, { allowHtml: true, width: '100%', height: '100%', page: 'enable', pageSize: 25 });
                })
                .fail(function (jqXHR, status, error) {
                    ErrorInFunc("GetEstuaries", status, error)
                });
        }
    }

    // Map

    interface MapPoint {
        id: number;
        name: string;
        link: string;
        url: string;
        latitude: number;
        longitude: number;
    }

    let map: google.maps.Map;
    let markers: google.maps.Marker[] = [];
    let mapPoints: MapPoint[];
    let mapBounds: google.maps.LatLngBounds;
    let mapFitted: boolean = false;

    export function InitMap() {
        let mapOpts: google.maps.MapOptions = {
            center: new google.maps.LatLng(-34, 25.5),
            zoom: 5
        };
        map = new google.maps.Map(document.getElementById('mapLocations'), mapOpts);
        UpdateMap(GetFilters());
        FitMap();
    }

    function UpdateMap(filters: Filters) {
        if (!filters) {
            filters = GetFilters();
        }
        $.post("/Search/GetMapData", filters)
            .done(function (json) {
                for (let i = 0; i < markers.length; i++) {
                    markers[i].setMap(null);
                }
                markers = [];
                mapPoints = json;
                mapBounds = new google.maps.LatLngBounds();
                for (let i = 0; i < mapPoints.length; i++) {
                    let mapPoint = mapPoints[i];
                    let marker = new google.maps.Marker({
                        position: { lat: mapPoint.latitude, lng: mapPoint.longitude },
                        map: map,
                        title: mapPoint.name
                    });
                    markers.push(marker);
                    mapBounds.extend(marker.getPosition());
                    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
                }
            })
            .fail(function (jqXHR, status, error) {
                ErrorInFunc("UpdateMap", status, error)
            });
    }

    export function FitMap(override: boolean = false) {
        if (override || (!mapFitted && (mapBounds != null) && !mapBounds.isEmpty())) {
            map.setCenter(mapBounds.getCenter());
            map.fitBounds(mapBounds);
            mapFitted = true;
        }
    }

    export function FixMap() {
        UpdateMap(GetFilters());
        FitMap(true);
    }

    // Filter updates

    class Filters {
        name: string;
        classification: number;
        region: number;
        condition: number;
        province: number;
    }

    function GetFilters(): Filters {
        let filters = new Filters();
        let name = <string>$("#Name").val();
        if (name != "") {
            filters.name = name;
        }
        let classification = <number>$("#Classification").val();
        if (classification) {
            filters.classification = classification;
        }
        let region = <number>$("#Region").val();
        if (region) {
            filters.region = region;
        }
        let condition = <number>$("#Condition").val();
        if (condition) {
            filters.condition = condition;
        }
        let province = <number>$("#Province").val();
        if (province) {
            filters.province = province;
        }
        return filters;
    }

    export function UpdateFilters() {
        //ShowWaiting();
        let filters = GetFilters();
        UpdateMap(filters);
        DrawEstuariesTable(filters);
        //setTimeout(HideWaiting(),2000);
    }
}