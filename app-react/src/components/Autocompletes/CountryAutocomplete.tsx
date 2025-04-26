import { Autocomplete, TextField } from "@mui/material";
import { enumApi } from "../../App";
import { useEffect, useState } from "react";
import { Country } from "../../api";

function CountryAutocomplete() {
  const [countries, setCountries] = useState<Country[]>([]);

  useEffect(() => {
    enumApi.enumEnumGetAllCountriesGet().then((result) => {
      setCountries(result.data);
    });
  }, []);

  return (
    <Autocomplete
      options={countries.map((country) => ({
        id: country.Id,
        label: country.Name,
      }))}
      renderInput={(params) => <TextField {...params} label="Country" />}
    />
  );
}

export default CountryAutocomplete;
