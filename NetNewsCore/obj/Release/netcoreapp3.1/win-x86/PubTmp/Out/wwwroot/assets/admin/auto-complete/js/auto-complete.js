
//an example of a json data source
function getJsonData() {
	var data = [{ "search_item": "Standard News Post" }, { "search_item": "News Video Post" }, { "search_item": "News Gallery Post" }, { "search_item": "News Audio Post" }, { "search_item": "Entertainment News Post" },
	{ "search_item": "Entertainment Video Post" }, { "search_item": "Entertainment Audio Post" }, { "search_item": "Manage Posts" }, { "search_item": "Post Reviews" }, { "search_item": "Post Statistics" }, { "search_item": "Entertainment Video Post" },
	{ "search_item": "Pending Reviews" }, { "search_item": "Review History" }, { "search_item": "Profile" }, { "search_item": "Settings" }, { "search_item": "Account Registrations" }, { "search_item": "Manage Accounts" },
	{ "search_item": "Manage Categories" }, { "search_item": "Manage SiteData" }]
	var result = data.map(function (val) {
		return val.search_item;
	}).join(',');
	return result;
}


//Function clears div then prints all that matches in a loop
function printList(array_list, search_val) {
	$('#auto-list').empty();

	var total_results = 0;
	for (var i = 0; i < array_list.length; i++) {
		$("#auto-list").show();

		if (array_list[i].toLowerCase().indexOf(search_val.toLowerCase().trim()) >= 0) {
			$('#auto-list').append("<a href='/Admin/" + array_list[i].replace(/ +/g, "") + "'><div class='card-body' id='" + array_list[i] + "'>" + array_list[i] + "</div></a>");
			total_results = parseInt(total_results) + 1;
		}
	}

	//If nothing found
	if (total_results == 0) {
		$('#auto-list').append("<div class='card-body'>No results found.</div>");
	}
}


//Clears and hide result div
function resetAll() {
	$('#auto-list').empty();
	$("#auto-list").hide();
}





//Comma separated data source
//var dataSource = "c++, c#, python, swift, kotlin, java, coldfusion, asp, ruby, rust, php, perl, pascal, pearl, scala";
var dataSource = getJsonData(); //Data from json.

//convert data to array
var dataArray = dataSource.trim().split(",");


$(document).ready(function () {
	$("#auto-list").hide();

	//Called when input is the the field. 0.5 sec delay
	$("#auto-input").keyup(function () {
		var searchVal = $("#auto-input").val();

		setTimeout(function () {
			if (searchVal.trim() != "") {
				printList(dataArray, searchVal);
			}
			else {
				resetAll();
			}
		}, 500);

	});
});

//Close results when you click outside
$(document).on('click', function (e) {
	if ($(e.target).closest("#auto-list").length === 0) {
		resetAll();
	}
});
