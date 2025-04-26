import { Autocomplete, TextField } from "@mui/material";
import { enumApi } from "../../App";
import { useEffect, useState } from "react";
import { CountryImport } from "../../api";

function CountryAutocomplete() {
  const [countries, setCountries] = useState<CountryImport[]>([]);
  const [filter, setFilter] = useState<string>("");

  useEffect(() => {
    enumApi.countryCountryGetAllGet(filter).then((result) => {
      setCountries(result.data);
    });
  }, [filter]);

  return (
    <Autocomplete
      options={
        countries?.map((country) => ({
          id: country.Id,
          label: country.Name,
        })) ?? []
      }
      onSelect={(event) => {
        console.log(event.target, event, event.target.value);
      }}
      onChange={(event, newValue) => {
        console.log(newValue);
      }}
      renderInput={(params) => (
        <TextField
          onChange={(e) => setFilter(e.target.value)}
          {...params}
          label="Country"
        />
      )}
    />
  );
}

export default CountryAutocomplete;
