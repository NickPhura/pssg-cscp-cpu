import { iHours } from "../models/hours.interface";

export function decodeToWeekDays(csvString: number[]): Partial<iHours> {
  return {
    monday: csvString.find((value) => value === 100000000) !== undefined ? true : false,
    tuesday: csvString.find((value) => value === 100000001) !== undefined ? true : false,
    wednesday: csvString.find((value) => value === 100000002) !== undefined ? true : false,
    thursday: csvString.find((value) => value === 100000003) !== undefined ? true : false,
    friday: csvString.find((value) => value === 100000004) !== undefined ? true : false,
    saturday: csvString.find((value) => value === 100000005) !== undefined ? true : false,
    sunday: csvString.find((value) => value === 100000006) !== undefined ? true : false,
  }
}
