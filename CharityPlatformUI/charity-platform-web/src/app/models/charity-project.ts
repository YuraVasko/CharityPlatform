export interface CreateCharityProject {
    id: string;
    title: string;
    organization: string;
    goal: number;
    alreadyDonated: number;
    dueDate: Date
}
