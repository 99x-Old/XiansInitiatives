export interface ReviewCriteria {
  id: string;
  reviewCycleId: string;
  createdAt: Date;
  criteria: string;
  weight: number;
  score: number;
  reviewerName: string;
  justification: string;
}
