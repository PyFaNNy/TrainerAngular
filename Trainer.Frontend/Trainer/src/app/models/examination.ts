import {Patient} from "./patient";

export class Examination {
  Id? : string;
  PatientId? : string;
  TypePhysicalActive?: number;
  Date: Date = new Date();
  Indicators?: number;
  Status? : number;
  Patient?: Patient;
}
