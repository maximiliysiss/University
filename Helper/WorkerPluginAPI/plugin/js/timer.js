

class Timer {

    constructor(diff) {
        if (diff < 4)
            diff = 4;
        this.hours = parseInt(diff / 1000 / 60 / 60);
        diff = diff - parseInt(diff / 1000 / 60 / 60) * 60 * 60 * 1000;
        this.minutes = parseInt(diff / 1000 / 60);
        diff = diff - parseInt(diff / 1000 / 60) * 60 * 1000;
        this.seconds = parseInt(diff / 1000);
    }

    incrementSecond() {


        this.seconds++;
        if (this.seconds >= 60) {
            this.minutes++;
            this.seconds = 0;
        }
        if (this.minutes >= 60) {
            this.hours++;
            this.minutes = 0;
        }
    }

    getAddition(data) {
        return data < 10 ? "0" : "";
    }

    toString() {
        return this.getAddition(this.hours) + this.hours + ":" + this.getAddition(this.minutes) + this.minutes + ":" + this.getAddition(this.seconds) + this.seconds;
    }

}