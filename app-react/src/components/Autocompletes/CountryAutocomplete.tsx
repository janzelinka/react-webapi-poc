import { Autocomplete, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import { AxiosPromise } from "axios";

interface BaseImport {
  Id?: number;
  Name?: string | null;
}

function GenericAutocomplete({
  getAll,
  label,
}: {
  getAll: (filter: string) => AxiosPromise<BaseImport[]>;
  label: string;
}) {
  const [items, setItems] = useState<BaseImport[]>([]);
  const [filter, setFilter] = useState<string>("");

  useEffect(() => {
    // countryApi.countryCountryGetAllGet(filter).then((result) => {
    //   setCountries(result.data);
    // });
    getAll(filter)
      .then((c) => setItems(c.data))
      .catch(console.error);
  }, [filter, getAll]);

  return (
    <Autocomplete
      options={
        items?.map((country) => ({
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
          label={label}
        />
      )}
    />
  );
}

export default GenericAutocomplete;
