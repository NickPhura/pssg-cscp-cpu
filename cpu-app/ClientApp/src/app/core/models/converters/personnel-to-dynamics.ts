import { DynamicsOrganizationContactPost } from "../../api/models/dynamicsOrganizationContactPost";
import { OrganizationPost } from "../../api/models/organizationPost";
import { iPerson } from "../person.interface";
import { convertPersonToDynamics } from "./person-to-dynamics";

//this is a mapper function for converting people into dynamics users
export function convertPersonnelToDynamics(
  userId: string,
  organizationId: string,
  people: iPerson[],
): OrganizationPost {
  const ppl: DynamicsOrganizationContactPost[] = [];
  for (let person of people) {
    const p = convertPersonToDynamics(
      person,
    ) as DynamicsOrganizationContactPost;
    ppl.push(p);
  }
  return {
    userBCeID: userId,
    businessBCeID: organizationId,
    staffCollection: ppl,
  };
}
