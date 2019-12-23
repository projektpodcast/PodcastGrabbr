<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>

  <xsl:param name="fileName" />
  <xsl:param name="updates" select="document($fileName)" />
  <xsl:variable name="updateShows" select="$updates/podcasts/show" />
  <xsl:variable name="updateIds" select="$updates/podcasts" />

  <!--/// AUTHOR DER KLASSE: PG
  ///-->

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="podcasts">
    <xsl:copy>
      <xsl:apply-templates select="$updateShows"/>
      <xsl:apply-templates select="show" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="//show">

    <xsl:copy>
      <xsl:choose>
        <xsl:when test="@sid != 'new'">
          <xsl:variable name="i" select="count(preceding-sibling::show)" />
          <xsl:attribute name="sid">
            <xsl:value-of select="generate-id(node())"/>
          </xsl:attribute>
          <xsl:apply-templates select="child::*"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:variable name="i" select="count(preceding-sibling::show)" />
          <xsl:attribute name="sid">
            <xsl:value-of select="generate-id(node())"/>
          </xsl:attribute>
          <xsl:apply-templates select="child::*"/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:copy>

  </xsl:template>


</xsl:stylesheet>