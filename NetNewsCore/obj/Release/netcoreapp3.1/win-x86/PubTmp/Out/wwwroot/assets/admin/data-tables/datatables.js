$(document).ready(function() {
  $("#dataTable").DataTable({
    responsive: true,

    columnDefs: [
      {
        responsivePriority: 1,
        targets: 0
      },
      {
        responsivePriority: 2,
        targets: -1
      }
    ],

    "drawCallback": function( settings ) {
        //run preset actions here
    }

  });

//set datatable by class 
 $('.dataTable').DataTable();

  //datatable default setup
  $(".dataTables_filter input")
    .attr("placeholder", "Search here...")
    .css({
      width: "300px",
      display: "inline-block"
    });

    $('[data-toggle="tooltip"]').tooltip();


});