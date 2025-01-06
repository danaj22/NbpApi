import apiClient from "./apiClient";

export const getLatestRates = (tableName: string) => {
  return apiClient.get(`/exchangeRates/latest?tableName=${tableName}`);
};

export const getRatesByDate = (tableName: string, date: string) => {
  return apiClient.get(
    `/exchangeRates?tableName=${tableName}&selectedDate=${date}`
  );
};

export const getAvailableDates = (tableName: string) => {
  return apiClient.get(`/exchangeRates/availableDates?tableName=${tableName}`);
};
