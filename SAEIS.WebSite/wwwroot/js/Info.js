var Info;
(function (Info) {
    // Errors
    function ShowWaiting() {
        //$("#modalLoading").modal("show");
    }
    Info.ShowWaiting = ShowWaiting;
    function HideWaiting() {
        //$("#modalLoading").modal("hide");
    }
    Info.HideWaiting = HideWaiting;
    function ErrorInFunc(method, status, error) {
        HideWaiting();
        alert("Error in " + method + " Status: " + status + " Error: " + error);
    }
    // Estuary
    var estuaryId;
    function SetEstuaryId(id) {
        estuaryId = id;
    }
    Info.SetEstuaryId = SetEstuaryId;
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
    var issues;
    var issuesTable;
    function DrawIssuesTable() {
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);
        function drawTable() {
            $.getJSON("/Info/" + estuaryId + "/Issues")
                .done(function (json) {
                issues = json;
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Type');
                data.addColumn('string', 'Header');
                for (var i = 0; i < issues.length; i++) {
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
    }
    Info.DrawIssuesTable = DrawIssuesTable;
    function IssueSelected() {
        var selection = issuesTable.getSelection();
        DrawIssue(selection[0].row);
    }
    function DrawIssue(i) {
        $("#Issue_Details").text("Details - " + issues[i].header);
        $("#Issue_Notes").val(issues[i].notes);
        $("#Issue_Responses").val(issues[i].responses);
    }
    // Literature
    var LiteratureFilters = /** @class */ (function () {
        function LiteratureFilters() {
        }
        return LiteratureFilters;
    }());
    function GetLiteratureFilters() {
        var filters = new LiteratureFilters();
        var type = $("#LiteratureType").val();
        if (type != "") {
            filters.type = type;
        }
        var subType = $("#LiteratureSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }
    function UpdateLiteratureFilters() {
        var filters = GetLiteratureFilters();
        DrawLiteratureTable(filters);
    }
    Info.UpdateLiteratureFilters = UpdateLiteratureFilters;
    function DrawLiteratureTable(filters) {
        if (!filters) {
            filters = GetLiteratureFilters();
        }
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);
        function drawTable() {
            $.post("/Info/" + estuaryId + "/Literature", filters)
                .done(function (json) {
                var items = json;
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Type');
                data.addColumn('string', 'Sub Type');
                data.addColumn('string', 'Author');
                data.addColumn('string', 'Year');
                data.addColumn('string', 'Title');
                for (var i = 0; i < items.length; i++) {
                    data.addRow([items[i].type, items[i].subType, items[i].author, items[i].year, items[i].title]);
                }
                var table = new google.visualization.Table(document.getElementById('tableLiterature'));
                table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
            })
                .fail(function (jqXHR, status, error) {
                ErrorInFunc("GetLiterature", status, error);
            });
        }
    }
    Info.DrawLiteratureTable = DrawLiteratureTable;
    // Maps
    var MapsFilters = /** @class */ (function () {
        function MapsFilters() {
        }
        return MapsFilters;
    }());
    function GetMapsFilters() {
        var filters = new MapsFilters();
        var type = $("#MapsType").val();
        if (type != "") {
            filters.type = type;
        }
        var subType = $("#MapsSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }
    function UpdateMapsFilters() {
        var filters = GetMapsFilters();
        DrawMapsTable(filters);
    }
    Info.UpdateMapsFilters = UpdateMapsFilters;
    function DrawMapsTable(filters) {
        if (!filters) {
            filters = GetMapsFilters();
        }
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);
        function drawTable() {
            $.post("/Info/" + estuaryId + "/Maps", filters)
                .done(function (json) {
                var items = json;
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Type');
                data.addColumn('string', 'Sub Type');
                data.addColumn('string', 'Name');
                for (var i = 0; i < items.length; i++) {
                    data.addRow([items[i].type, items[i].subType, items[i].name]);
                }
                var table = new google.visualization.Table(document.getElementById('tableMaps'));
                table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
            })
                .fail(function (jqXHR, status, error) {
                ErrorInFunc("GetMaps", status, error);
            });
        }
    }
    Info.DrawMapsTable = DrawMapsTable;
    // Images
    var ImagesFilters = /** @class */ (function () {
        function ImagesFilters() {
        }
        return ImagesFilters;
    }());
    function GetImagesFilters() {
        var filters = new ImagesFilters();
        var type = $("#ImagesType").val();
        if (type != "") {
            filters.type = type;
        }
        var subType = $("#ImagesSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }
    function UpdateImagesFilters() {
        var filters = GetImagesFilters();
        DrawImagesTable(filters);
    }
    Info.UpdateImagesFilters = UpdateImagesFilters;
    function DrawImagesTable(filters) {
        if (!filters) {
            filters = GetImagesFilters();
        }
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);
        function drawTable() {
            $.post("/Info/" + estuaryId + "/Images", filters)
                .done(function (json) {
                var items = json;
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Type');
                data.addColumn('string', 'Sub Type');
                data.addColumn('string', 'Date');
                data.addColumn('string', 'Name');
                data.addColumn('string', 'Source');
                data.addColumn('string', 'Reference');
                data.addColumn('string', 'Notes');
                for (var i = 0; i < items.length; i++) {
                    data.addRow([items[i].type, items[i].subType, items[i].date, items[i].name, items[i].source, items[i].reference, items[i].notes]);
                }
                var table = new google.visualization.Table(document.getElementById('tableImages'));
                table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
            })
                .fail(function (jqXHR, status, error) {
                ErrorInFunc("GetImages", status, error);
            });
        }
    }
    Info.DrawImagesTable = DrawImagesTable;
    // Datasets
    var DatasetsFilters = /** @class */ (function () {
        function DatasetsFilters() {
        }
        return DatasetsFilters;
    }());
    function GetDatasetsFilters() {
        var filters = new DatasetsFilters();
        var type = $("#DatasetsType").val();
        if (type != "") {
            filters.type = type;
        }
        var subType = $("#DatasetsSubType").val();
        if (subType != "") {
            filters.subType = subType;
        }
        return filters;
    }
    function UpdateDatasetsFilters() {
        var filters = GetDatasetsFilters();
        DrawDatasetsTable(filters);
    }
    Info.UpdateDatasetsFilters = UpdateDatasetsFilters;
    function DrawDatasetsTable(filters) {
        if (!filters) {
            filters = GetDatasetsFilters();
        }
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);
        function drawTable() {
            $.post("/Info/" + estuaryId + "/Datasets", filters)
                .done(function (json) {
                var items = json;
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Type');
                data.addColumn('string', 'Sub Type');
                data.addColumn('string', 'Variable');
                data.addColumn('string', 'Date');
                data.addColumn('string', 'Dataset');
                for (var i = 0; i < items.length; i++) {
                    data.addRow([items[i].type, items[i].subType, items[i].variable, items[i].date, items[i].dataset]);
                }
                var table = new google.visualization.Table(document.getElementById('tableDatasets'));
                table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
            })
                .fail(function (jqXHR, status, error) {
                ErrorInFunc("GetDatasets", status, error);
            });
        }
    }
    Info.DrawDatasetsTable = DrawDatasetsTable;
})(Info || (Info = {}));
//# sourceMappingURL=Info.js.map