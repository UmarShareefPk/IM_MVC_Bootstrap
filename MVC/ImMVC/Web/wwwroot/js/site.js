//console.log("hi");
//function getUserNameById(id) {
//   let num =  Math.floor(Math.random() * 11);  
//    switch (num) {
//        case 0:
//            return "John Wick";
//        case 1:
//            return "Ali Ashraf";
//        case 2:
//            return "Qamar Shareef";
//        case 3:
//            return "Imran Khan";
//        case 4:
//            return "Nawaz Shareef";
//        case 5:
//            return "Abdullah Waseem";
//        case 6:
//            return "Maryam Umar";
//        case 7:
//            return "Fatima Waseem";
//        case 8:
//            return "Ayesha Waseem";
//        default:
//            return "Umar Shareef";
//    }
//}

function getStatusByCode(code) {
    switch (code) {
        case "N":
            return "New";
        case "I":
            return "In Progress";
        case "A":
            return "Approved";
        case "C":
            return "Closed";
        default:
            return "Invalid";
    }
}

function auto_grow(element) { // for textareas
    element.style.height = "5px";
    element.style.height = (element.scrollHeight) + "px";
}

function getUserNameById(userId) {
    let allUsers = JSON.parse(localStorage.getItem("allUsers"));
    let user = allUsers.find((user) => {
        return user.Id === userId;
    });
    return user.FirstName + " " + user.LastName;    
}

function getAssingeesBySearch(search) {
    let allAssignees = JSON.parse(localStorage.getItem("allUsers"));

    let newList = [];
    if (search !== "") {
        newList = allAssignees.filter((assignee) => {
            return (
                assignee.FirstName.toUpperCase().startsWith( search.toUpperCase() )
                ||
                assignee.LastName.toUpperCase().startsWith(search.toUpperCase())
              );
        });
    }

    if (newList !== undefined && newList.length !== 0) {
        //check if there is atlease one assignee
        newList = [].concat(newList);
    } else {
        //if search found nothing, show all assignees
        newList = allAssignees.slice(0, allAssignees.length);
    }

    return newList;
}
