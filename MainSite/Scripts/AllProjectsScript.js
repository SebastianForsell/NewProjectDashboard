"use strict";
$(document).ready(function () {
    var currentID;
    var itemTable;
    var createProjectButton = '#createProjectButton';
    var mainTable = $('#projectsTable').DataTable();
    var progressBar = document.getElementsByTagName("progress");
    for (var i = 0; i < progressBar.length; i++) {

        progressBar[i].addEventListener("click", function () {
            currentID = this.id;
            //$('#itemTableView').Load(PartialURL.url, currentID);
            $.ajax({
                url: partialURL.url,
                type: 'POST',
                data: {
                    ID: currentID
                },
                cache: false,
                success: function (data) {
                    $('#createProjectButton').remove();
                    mainTable.destroy();
                    $('#projectsTable').remove();
                    $('#itemTableView').html(data)
                    itemTable = $('#projectItemTable').DataTable();
                },
                error: function (data) {
                    alert("error");
                    console.log(data);
                }
            })
        });
    }
    $(document).on('click', createProjectButton, function(){
        $('#createProjectWindow').dialog({
            modal: true,
            open: function () {

            },
            buttons: {
                "Lägg till": function () {
                    var companyID = $('#companyDropDown option:selected').val();
                    var projectName = $('#projectName').val();
                    var totalTime = $('#totalTime').val();
                    var t = $('#tInput').val();
                    var utf = $('#utfInput').val();
                    var deadline = $('#deadlineInput').val();
                    $.ajax({
                        url: partialURL.updateProjectsUrl,
                        type: 'POST',
                        data: {
                            customerID: companyID,
                            Name: projectName,
                            Status: 'Offert', //ÄNDRA SENARE!
                            TotalTid: totalTime,
                            Deadline: deadline,
                            T: t,
                            UTF: utf
                        },
                        cache: false,
                        success: function (data) {
                            $('.createProjectWindow').dialog("close");
                            mainTable.destroy();
                            $('#createProjectButton').remove();
                            $('#projectsTable').remove();
                            $('#createProjectWindow').remove();
                            
                            $('#updatedProjectTable').html(data);
                            mainTable = $('#projectsTable').DataTable();
                        },
                        error: function (data) {
                            console.log(data)
                        }

                    })
                },
                "Avbryt": function () {
                    $(this).dialog("close");
                }
            }
        });
    })
    $(document).on('click', '#createItemButton', function () {
        $('#createProjectItemWindow').dialog({
            modal: true,
            open: function () {

            },
            buttons: {
                "Lägg till": function () {
                    var projectID = $('#projectIDInput').val();
                    var projectItemName = $('#nameInput').val();
                    var totalTime = $('#totalInput').val();
                    var deadline = $('#dateInput').val();
                    $.ajax({
                        url: partialURL.updateProjectItemsUrl,
                        type: 'POST',
                        data: {
                            ProjektID: projectID,
                            Name: projectItemName,
                            UserID: 2, //ÄNDRA SENARE!
                            UtveckladeTimmar: 5, //ÄNDRA SENARE!
                            TotalTid: totalTime,
                            Deadline: deadline,
                            id: projectID
                        },
                        cache: false,
                        success: function (data) {
                            $('#createProjectItemWindow').dialog("close");
                            $('#projectItemTable').remove();
                            $('#itemTableView').html(data);
                            itemTable.destroy();
                            itemTable = $('#projectItemTable').DataTable();
                        },
                        error: function (data) {
                            console.log(data)
                        }

                    })
                },
                "Avbryt": function () {
                    $(this).dialog("close");
                }
            }
        });
    })
    $(document).on('click', '.addHours', function () {
        var currid = $(this).closest("button").attr('value');
        //var popup = $('.addHourWindow').dialog();
        $('.addHourWindow').dialog({
            modal: true,
            open: function () {

            },
            buttons: {
                "Lägg till": function () {
                    var addHourform = $('#addHoursForm');
                    var userID = $('#userID').val();
                    var hours = $('#addHoursText').val();
                    var comment = $('#addComment').val();
                    var deadline = $('#addDate').val();
                    $.ajax({
                        url: partialURL.updateItemUrl,
                            type: 'POST',
                            data: {
                                ItemID: Number(currid),
                                UserID: Number(userID),
                                AntalTimmar: Number(hours),
                                Kommentar: comment,
                                DateCreated: deadline.valueAsDate,
                                id: currentID
                            },
                            cache: false,
                            success: function (data) {
                                $('.addHourWindow').dialog("close");
                                $('#createItemButton').remove();
                                $('#projectItemTable').remove();
                                $('#itemTableView').html(data);
                                itemTable.destroy();
                                itemTable = $('#projectItemTable').DataTable();
                            },
                            error: function (data) {
                                console.log(data);
                            }
                        })
                },
                "Avbryt": function () {
                    $(this).dialog("close");
                }
            }
        });
    })
});
