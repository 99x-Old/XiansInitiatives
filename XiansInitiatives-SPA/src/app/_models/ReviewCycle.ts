

export interface ReviewCycle {
  id: string;
  createdAt: Date;
  scheduledAt: Date;
  overallComment: string;
  description: string;
  initiativeId: string;
  evaluatorId: string;
}
