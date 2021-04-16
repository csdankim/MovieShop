import { MovieCard } from './movie-card';

export interface CastDetail {
  id: number;
  name: string;
  gender: string;
  tmdbUrl: string;
  profilePath: string;
  movies: MovieCard[];
}