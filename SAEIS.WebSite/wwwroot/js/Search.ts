﻿namespace Search {
    // Errors

    export function ShowWaiting() {
        //let wp = $("#waiting").data("ejWaitingPopup");
        //wp.show();
    }

    export function HideWaiting() {
        //let wp = $("#waiting").data("ejWaitingPopup");
        //wp.hide();
    }

    function ErrorInFunc(method: string, status: string, error: string) {
        HideWaiting();
        alert("Error in " + method + " Status: " + status + " Error: " + error);
    }

    // Table

    interface TableItem {
        id: number;
        name: string;
        province: string;
        classification: string;
    }

    export function DrawTable(filters: Filters) {
        if (!filters) {
            filters = GetFilters();
        }

        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            $.post("/Search/GetTableData",filters)
                .done(function (json) {
                    var data = new google.visualization.DataTable();
                    data.addColumn('number', '#');
                    data.addColumn('string', 'Name');
                    data.addColumn('string', 'Province');
                    data.addColumn('string', 'Classification');

                    let dataValues: TableItem[] = json;
                    for (let i = 0; i < dataValues.length; i++) {
                        data.addRow([dataValues[i].id, dataValues[i].name, dataValues[i].province, dataValues[i].classification]);
                    }
                    var table = new google.visualization.Table(document.getElementById('table_div'));
                    table.draw(data, { allowHtml: true, width: '100%', height: '100%' });
                })
                .fail(function (jqXHR, status, error) {
                    ErrorInFunc("GetTableData", status, error)
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

    export function UpdateMap(filters: Filters) {
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
                    //if (mapPoint.IsSelected) {
                    //    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
                    //}
                    //else {
                    //    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png');
                    //}
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
        let filters = GetFilters();
        DrawTable(filters);
        UpdateMap(filters);
    }
}