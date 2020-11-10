// Waiting
export function ShowWaiting() {
    //$("#modalLoading").modal("show");
}
export function HideWaiting() {
    //$("#modalLoading").modal("hide");
}
// Errors
function ErrorInFunc(method, status, error) {
    HideWaiting();
    alert("Error in " + method + " Status: " + status + " Error: " + error);
}
// Estuary
let estuaryId;
export function SetEstuaryId(id) {
    estuaryId = id;
}
// Map
export class MapPoint {
    constructor(name, latitude, longitude) {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
    }
}
let map;
let mapPoint;
export function InitMap() {
    const mapOpts = {
        center: new google.maps.LatLng(mapPoint.latitude, mapPoint.longitude),
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.SATELLITE
    };
    map = new google.maps.Map(document.getElementById('mapLocations'), mapOpts);
    const marker = new google.maps.Marker({
        position: { lat: mapPoint.latitude, lng: mapPoint.longitude },
        map: map,
        title: mapPoint.name
    });
    marker.setIcon('https://maps.google.com/mapfiles/ms/icons/green-dot.png');
}
export function SetMapPoint(name, latitude, longitude) {
    mapPoint = new MapPoint(name, latitude, longitude);
}
let issues;
let issuesTable;
function DrawIssue(i) {
    $("#Issue_Details").text("Details - " + issues[i].header);
    $("#Issue_Notes").val(issues[i].notes);
    $("#Issue_Responses").val(issues[i].responses);
}
function IssueSelected() {
    const selection = issuesTable.getSelection();
    DrawIssue(selection[0].row);
}
export function DrawIssuesTable() {
    function drawTable() {
        $.getJSON("/Info/" + estuaryId + "/Issues")
            .done(function (json) {
            issues = json;
            const data = new google.visualization.DataTable();
            data.addColumn('string', 'Type');
            data.addColumn('string', 'Header');
            for (let i = 0; i < issues.length; i++) {
                data.addRow([issues[i].type, issues[i].header]);
            }
            issuesTable = new google.visualization.Table(document.getElementById('tableIssues'));
            issuesTable.draw(data, { width: '100%', height: '100%' });
            google.visualization.events.addListener(issuesTable, 'select', IssueSelected);
            if (issues.length > 0) {
                DrawIssue(0);
            }
        })
            .fail(function (jqXHR, status, error) {
            ErrorInFunc("GetIssues", status, error);
        });
    }
    google.charts.load('current', { 'packages': ['table'] });
    google.charts.setOnLoadCallback(drawTable);
}
// Literature
class LiteratureFilters {
}
function GetLiteratureFilters() {
    const filters = new LiteratureFilters();
    const type = $("#LiteratureType").val();
    if (type !== "") {
        filters.type = type;
    }
    const subType = $("#LiteratureSubType").val();
    if (subType !== "") {
        filters.subType = subType;
    }
    return filters;
}
export function DrawLiteratureTable(filters) {
    if (!filters) {
        filters = GetLiteratureFilters();
    }
    function drawTable() {
        $.post("/Info/" + estuaryId + "/Literature", filters)
            .done(function (json) {
            const items = json;
            const data = new google.visualization.DataTable();
            data.addColumn('string', 'Type');
            data.addColumn('string', 'Sub Type');
            data.addColumn('string', 'Author');
            data.addColumn('string', 'Year');
            data.addColumn('string', 'Title');
            for (let i = 0; i < items.length; i++) {
                data.addRow([items[i].type, items[i].subType, items[i].author, items[i].year, items[i].title]);
            }
            const table = new google.visualization.Table(document.getElementById('tableLiterature'));
            table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
        })
            .fail(function (jqXHR, status, error) {
            ErrorInFunc("GetLiterature", status, error);
        });
    }
    google.charts.load('current', { 'packages': ['table'] });
    google.charts.setOnLoadCallback(drawTable);
}
export function UpdateLiteratureFilters() {
    const filters = GetLiteratureFilters();
    DrawLiteratureTable(filters);
}
// Maps
class MapsFilters {
}
function GetMapsFilters() {
    const filters = new MapsFilters();
    const type = $("#MapsType").val();
    if (type !== "") {
        filters.type = type;
    }
    const subType = $("#MapsSubType").val();
    if (subType !== "") {
        filters.subType = subType;
    }
    return filters;
}
export function DrawMapsTable(filters) {
    if (!filters) {
        filters = GetMapsFilters();
    }
    function drawTable() {
        $.post("/Info/" + estuaryId + "/Maps", filters)
            .done(function (json) {
            const items = json;
            const data = new google.visualization.DataTable();
            data.addColumn('string', 'Type');
            data.addColumn('string', 'Sub Type');
            data.addColumn('string', 'Name');
            for (let i = 0; i < items.length; i++) {
                data.addRow([items[i].type, items[i].subType, items[i].name]);
            }
            const table = new google.visualization.Table(document.getElementById('tableMaps'));
            table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
        })
            .fail(function (jqXHR, status, error) {
            ErrorInFunc("GetMaps", status, error);
        });
    }
    google.charts.load('current', { 'packages': ['table'] });
    google.charts.setOnLoadCallback(drawTable);
}
export function UpdateMapsFilters() {
    const filters = GetMapsFilters();
    DrawMapsTable(filters);
}
// Images
class ImagesFilters {
}
function GetImagesFilters() {
    const filters = new ImagesFilters();
    const type = $("#ImageType").val();
    if (type !== "") {
        filters.type = type;
    }
    const subType = $("#ImageSubType").val();
    if (subType !== "") {
        filters.subType = subType;
    }
    return filters;
}
export function DrawImagesTable(filters) {
    if (!filters) {
        filters = GetImagesFilters();
    }
    function drawTable() {
        $.post("/Info/" + estuaryId + "/Images", filters)
            .done(function (json) {
            const items = json;
            const data = new google.visualization.DataTable();
            data.addColumn('string', 'Type');
            data.addColumn('string', 'Sub Type');
            data.addColumn('string', 'Date');
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Source');
            data.addColumn('string', 'Reference');
            data.addColumn('string', 'Notes');
            for (let i = 0; i < items.length; i++) {
                data.addRow([items[i].type, items[i].subType, items[i].date, items[i].name, items[i].source, items[i].reference, items[i].notes]);
            }
            const table = new google.visualization.Table(document.getElementById('tableImages'));
            table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
        })
            .fail(function (jqXHR, status, error) {
            ErrorInFunc("GetImages", status, error);
        });
    }
    google.charts.load('current', { 'packages': ['table'] });
    google.charts.setOnLoadCallback(drawTable);
}
// Datasets
class DatasetsFilters {
}
function GetDatasetsFilters() {
    const filters = new DatasetsFilters();
    const type = $("#DatasetsType").val();
    if (type !== "") {
        filters.type = type;
    }
    const subType = $("#DatasetsSubType").val();
    if (subType !== "") {
        filters.subType = subType;
    }
    return filters;
}
export function DrawDatasetsTable(filters) {
    if (!filters) {
        filters = GetDatasetsFilters();
    }
    function drawTable() {
        $.post("/Info/" + estuaryId + "/Datasets", filters)
            .done(function (json) {
            const items = json;
            const data = new google.visualization.DataTable();
            data.addColumn('string', 'Type');
            data.addColumn('string', 'Sub Type');
            data.addColumn('string', 'Variable');
            data.addColumn('string', 'Date');
            data.addColumn('string', 'Dataset');
            for (let i = 0; i < items.length; i++) {
                data.addRow([items[i].type, items[i].subType, items[i].variable, items[i].date, items[i].dataset]);
            }
            const table = new google.visualization.Table(document.getElementById('tableDatasets'));
            table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
        })
            .fail(function (jqXHR, status, error) {
            ErrorInFunc("GetDatasets", status, error);
        });
    }
    google.charts.load('current', { 'packages': ['table'] });
    google.charts.setOnLoadCallback(drawTable);
}
export function UpdateDatasetsFilters() {
    const filters = GetDatasetsFilters();
    DrawDatasetsTable(filters);
}
export function UpdateImagesFilters() {
    const filters = GetImagesFilters();
    DrawImagesTable(filters);
}
// Events
export function SetEventListeners() {
    document.getElementById("DatasetType")?.addEventListener("change", () => UpdateDatasetsFilters());
    document.getElementById("DatasetSubType")?.addEventListener("change", () => UpdateDatasetsFilters());
    document.getElementById("ImageType")?.addEventListener("change", () => UpdateImagesFilters());
    document.getElementById("ImageSubType")?.addEventListener("change", () => UpdateImagesFilters());
    document.getElementById("LiteratureType")?.addEventListener("change", () => UpdateLiteratureFilters());
    document.getElementById("LiteratureSubType")?.addEventListener("change", () => UpdateLiteratureFilters());
    document.getElementById("MapType")?.addEventListener("change", () => UpdateMapsFilters());
    document.getElementById("MapSubType")?.addEventListener("change", () => UpdateMapsFilters());
}
