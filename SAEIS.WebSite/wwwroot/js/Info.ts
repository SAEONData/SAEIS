namespace Info {
    // Errors

    export function ShowWaiting() {
        //$("#modalLoading").modal("show");
    }

    export function HideWaiting() {
        //$("#modalLoading").modal("hide");
    }

    function ErrorInFunc(method: string, status: string, error: string) {
        HideWaiting();
        alert("Error in " + method + " Status: " + status + " Error: " + error);
    }

    // Estuary
    let estuaryId: number;

    export function SetEstuaryId(id: number) {
        estuaryId = id;
    }

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

    // Issues

    interface IssueItem {
        id: number;
        type: string;
        header: string;
        notes: string;
        responses: string;
    }

    let issues: IssueItem[];
    let issuesTable: google.visualization.Table;

    export function DrawIssuesTable() {
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            $.getJSON("/Info/" + estuaryId + "/Issues")
                .done(function (json) {
                    issues = json;
                    var data = new google.visualization.DataTable();
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
                    ErrorInFunc("GetIssues", status, error)
                });
        }
    }

    function IssueSelected() {
        var selection = issuesTable.getSelection();
        DrawIssue(selection[0].row);
    }

    function DrawIssue(i: number) {
        $("#Issue_Details").text("Details - " + issues[i].header);
        $("#Issue_Notes").val(issues[i].notes);
        $("#Issue_Responses").val(issues[i].responses);
    }

    // Literature

    class LiteratureFilters {
        type: string;
        subType: string;
    }

    function GetLiteratureFilters(): LiteratureFilters {
        let filters = new LiteratureFilters();
        let type = <string>$("#LiteratureType").val();
        if (type != "") {
            filters.type = type;
        }
        let subType = <string>$("#LiteratureSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }

    export function UpdateLiteratureFilters() {
        let filters = GetLiteratureFilters();
        DrawLiteratureTable(filters);
    }


    interface LiteratureItem {
        id: number;
        type: string;
        subType: string;
        author: string;
        year: string;
        title: string;
    }

    export function DrawLiteratureTable(filters: LiteratureFilters) {
        if (!filters) {
            filters = GetLiteratureFilters();
        }

        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            $.post("/Info/" + estuaryId + "/Literature",filters)
                .done(function (json) {
                    let items: LiteratureItem[] = json;
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Type');
                    data.addColumn('string', 'Sub Type');
                    data.addColumn('string', 'Author');
                    data.addColumn('string', 'Year');
                    data.addColumn('string', 'Title');
                    for (let i = 0; i < items.length; i++) {
                        data.addRow([items[i].type, items[i].subType, items[i].author, items[i].year, items[i].title]);
                    }
                    let table = new google.visualization.Table(document.getElementById('tableLiterature'));
                    table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
                })
                .fail(function (jqXHR, status, error) {
                    ErrorInFunc("GetLiterature", status, error)
                });
        }
    }

    // Maps

    class MapsFilters {
        type: string;
        subType: string;
    }

    function GetMapsFilters(): MapsFilters {
        let filters = new MapsFilters();
        let type = <string>$("#MapsType").val();
        if (type != "") {
            filters.type = type;
        }
        let subType = <string>$("#MapsSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }

    export function UpdateMapsFilters() {
        let filters = GetMapsFilters();
        DrawMapsTable(filters);
    }


    interface MapItem {
        id: number;
        type: string;
        subType: string;
        name: string;
    }

    export function DrawMapsTable(filters: MapsFilters) {
        if (!filters) {
            filters = GetMapsFilters();
        }

        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            $.post("/Info/" + estuaryId + "/Maps", filters)
                .done(function (json) {
                    let items: MapItem[] = json;
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Type');
                    data.addColumn('string', 'Sub Type');
                    data.addColumn('string', 'Name');
                    for (let i = 0; i < items.length; i++) {
                        data.addRow([items[i].type, items[i].subType, items[i].name]);
                    }
                    let table = new google.visualization.Table(document.getElementById('tableMaps'));
                    table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
                })
                .fail(function (jqXHR, status, error) {
                    ErrorInFunc("GetMaps", status, error)
                });
        }
    }

    // Images

    class ImagesFilters {
        type: string;
        subType: string;
    }

    function GetImagesFilters(): ImagesFilters {
        let filters = new ImagesFilters();
        let type = <string>$("#ImagesType").val();
        if (type != "") {
            filters.type = type;
        }
        let subType = <string>$("#ImagesSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }

    export function UpdateImagesFilters() {
        let filters = GetImagesFilters();
        DrawImagesTable(filters);
    }


    interface ImageItem {
        id: number;
        type: string;
        subType: string;
        name: string;
        date: string;
        source: string;
        reference: string;
        notes: string;
    }

    export function DrawImagesTable(filters: ImagesFilters) {
        if (!filters) {
            filters = GetImagesFilters();
        }

        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            $.post("/Info/" + estuaryId + "/Images", filters)
                .done(function (json) {
                    let items: ImageItem[] = json;
                    var data = new google.visualization.DataTable();
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
                    let table = new google.visualization.Table(document.getElementById('tableImages'));
                    table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
                })
                .fail(function (jqXHR, status, error) {
                    ErrorInFunc("GetImages", status, error)
                });
        }
    }

    // Datasets

    class DatasetsFilters {
        type: string;
        subType: string;
    }

    function GetDatasetsFilters(): DatasetsFilters {
        let filters = new DatasetsFilters();
        let type = <string>$("#DatasetsType").val();
        if (type != "") {
            filters.type = type;
        }
        let subType = <string>$("#DatasetsSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }

    export function UpdateDatasetsFilters() {
        let filters = GetDatasetsFilters();
        DrawDatasetsTable(filters);
    }


    interface DatasetItem {
        id: number;
        type: string;
        subType: string;
        variable: string;
        date: string;
        dataset: string;
    }

    export function DrawDatasetsTable(filters: DatasetsFilters) {
        if (!filters) {
            filters = GetDatasetsFilters();
        }

        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            $.post("/Info/" + estuaryId + "/Datasets", filters)
                .done(function (json) {
                    let items: DatasetItem[] = json;
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Type');
                    data.addColumn('string', 'Sub Type');
                    data.addColumn('string', 'Variable');
                    data.addColumn('string', 'Date');
                    data.addColumn('string', 'Dataset');
                    for (let i = 0; i < items.length; i++) {
                        data.addRow([items[i].type, items[i].subType, items[i].variable, items[i].date, items[i].dataset]);
                    }
                    let table = new google.visualization.Table(document.getElementById('tableDatasets'));
                    table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
                })
                .fail(function (jqXHR, status, error) {
                    ErrorInFunc("GetDatasets", status, error)
                });
        }
    }


}