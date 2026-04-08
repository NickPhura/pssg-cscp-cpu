import { Component, OnInit, inject } from "@angular/core";
import * as moment from "moment-timezone";
import { ConfigurationStore } from "./core/store/configuration.store";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
  standalone: false,
})
export class AppComponent implements OnInit {
  title = "cpu-public-app";
  readonly configStore = inject(ConfigurationStore);

  ngOnInit(): void {
    this.configStore.load();
  }

  isOutage() {
    const configuration = this.configStore.configuration();
    if (
      !configuration ||
      !configuration.outageEndDate ||
      !configuration.outageStartDate ||
      !configuration.outageMessage
    ) {
      return false;
    }
    const currentDate = moment().tz("America/Vancouver");
    const outageStartDate = moment(configuration.outageStartDate).tz(
      "America/Vancouver",
    );
    const outageEndDate = moment(configuration.outageEndDate).tz(
      "America/Vancouver",
    );
    return currentDate.isBetween(outageStartDate, outageEndDate, null, "[]");
  }

  generateOutageDateMessage(): string {
    const configuration = this.configStore.configuration();
    if (!configuration) return "";
    const startDate = moment(configuration.outageStartDate)
      .tz("America/Vancouver")
      .format("MMMM Do YYYY, h:mm a");
    const endDate = moment(configuration.outageEndDate)
      .tz("America/Vancouver")
      .format("MMMM Do YYYY, h:mm a");
    return (
      "The system will be down for maintenance from " +
      startDate +
      " to " +
      endDate
    );
  }
}
