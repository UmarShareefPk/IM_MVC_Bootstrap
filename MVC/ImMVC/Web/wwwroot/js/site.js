﻿console.log("hi");
function getUserNameById(id) {
   let num =  Math.floor(Math.random() * 11);  
    switch (num) {
        case 0:
            return "John Wick";
        case 1:
            return "Ali Ashraf";
        case 2:
            return "Qamar Shareef";
        case 3:
            return "Imran Khan";
        case 4:
            return "Nawaz Shareef";
        case 5:
            return "Abdullah Waseem";
        case 6:
            return "Maryam Umar";
        case 7:
            return "Fatima Waseem";
        case 8:
            return "Ayesha Waseem";
        default:
            return "Umar Shareef";
    }
}

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