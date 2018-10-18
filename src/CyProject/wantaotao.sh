#!/bin/bash

in_fastq_file_path=$1
out_fasta_file_path=$2
echo $in_fastq_file_path
echo $out_fasta_file_path
#fastq_to_fasta -i Nanopore-data/f3.fastq -o Nanopore-data/f3.fasta
fastq_to_fasta -i $in_fastq_file_path -o $out_fasta_file_path