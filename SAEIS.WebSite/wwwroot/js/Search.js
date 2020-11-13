// Waiting
export function ShowWaiting() {
    $("#modalLoading").modal("show");
}
export function HideWaiting() {
    $("#modalLoading").modal("hide");
}
// Errors
function ErrorInFunc(method, status, error) {
    HideWaiting();
    alert("Error in " + method + " Status: " + status + " Error: " + error);
}
// Filter updates
class Filters {
}
function GetFilters() {
    const filters = new Filters();
    const name = $("#Name").val();
    if (name !== "") {
        filters.name = name;
    }
    const classification = $("#Classification").val();
    if (classification) {
        filters.classification = classification;
    }
    const region = $("#Region").val();
    if (region) {
        filters.region = region;
    }
    const condition = $("#Condition").val();
    if (condition) {
        filters.condition = condition;
    }
    const province = $("#Province").val();
    if (province) {
        filters.province = province;
    }
    return filters;
}
function DrawEstuariesTable(filters) {
    if (!filters) {
        filters = GetFilters();
    }
    function drawTable() {
        $.post("/Search/GetEstuaries", filters)
            .done(function (json) {
            const data = new google.visualization.DataTable();
            data.addColumn('number', '#');
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Classification');
            data.addColumn('string', 'Region');
            data.addColumn('string', 'Condition');
            data.addColumn('string', 'Province');
            const items = json;
            for (let i = 0; i < items.length; i++) {
                data.addRow([items[i].id, items[i].name, items[i].classification, items[i].region, items[i].condition, items[i].province]);
            }
            const table = new google.visualization.Table(document.getElementById('tableEstuaries'));
            table.draw(data, { allowHtml: true, width: '100%', height: '100%', page: 'enable', pageSize: 25 });
        })
            .fail(function (jqXHR, status, error) {
            ErrorInFunc("GetEstuaries", status, error);
        });
    }
    google.charts.load('current', { 'packages': ['table'] });
    google.charts.setOnLoadCallback(drawTable);
}
let map;
let markers = [];
let mapPoints;
let mapBounds;
let mapFitted = false;
function UpdateMap(filters) {
    if (!map)
        return;
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
            const mapPoint = mapPoints[i];
            const marker = new google.maps.Marker({
                position: { lat: mapPoint.latitude, lng: mapPoint.longitude },
                map: map,
                title: mapPoint.name
            });
            markers.push(marker);
            mapBounds.extend(marker.getPosition());
            marker.setIcon('https://maps.google.com/mapfiles/ms/icons/green-dot.png');
            marker.addListener('click', function () {
                window.location.href = mapPoint.url;
            });
        }
    })
        .fail(function (jqXHR, status, error) {
        ErrorInFunc("UpdateMap", status, error);
    });
}
export function UpdateFilters() {
    //ShowWaiting();
    const filters = GetFilters();
    UpdateMap(filters);
    DrawEstuariesTable(filters);
    //setTimeout(HideWaiting(),2000);
}
export function InitMap() {
    const mapOpts = {
        center: new google.maps.LatLng(-30.913054, 24.669581),
        zoom: 6,
        mapTypeId: google.maps.MapTypeId.SATELLITE
    };
    map = new google.maps.Map(document.getElementById('mapLocations'), mapOpts);
    UpdateMap(GetFilters());
    //FitMap();
}
export function FitMap(override = false) {
    if (!map) {
        InitMap();
    }
    if (override || (!mapFitted && (mapBounds) && !mapBounds.isEmpty())) {
        map.setCenter(mapBounds.getCenter());
        map.fitBounds(mapBounds);
        mapFitted = true;
    }
}
export function FixMap() {
    UpdateMap(GetFilters());
    FitMap(true);
    alert(map.getCenter() + " " + map.getZoom());
}
// Event listeners
export function SetEventListeners() {
    document.getElementById("Name")?.addEventListener("keyup", () => UpdateFilters());
    document.getElementById("Classification")?.addEventListener("change", () => UpdateFilters());
    document.getElementById("Region")?.addEventListener("change", () => UpdateFilters());
    document.getElementById("Condition")?.addEventListener("change", () => UpdateFilters());
    document.getElementById("Province")?.addEventListener("change", () => UpdateFilters());
}
